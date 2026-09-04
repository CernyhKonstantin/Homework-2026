using System.Data;
using System.Text;
using System.Text.Json;
using HW_31._08._2026.Data;
using HW_31._08._2026.Helpers;
using HW_31._08._2026.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HW_31._08._2026.Services;

public sealed class OrderQueueConsumer : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly RabbitMqSettings _rabbitSettings;
    private readonly ILogger<OrderQueueConsumer> _logger;
    private IConnection? _connection;
    private IModel? _channel;

    public OrderQueueConsumer(
        IServiceScopeFactory scopeFactory,
        IOptions<RabbitMqSettings> rabbitOptions,
        ILogger<OrderQueueConsumer> logger)
    {
        _scopeFactory = scopeFactory;
        _rabbitSettings = rabbitOptions.Value;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _rabbitSettings.HostName,
            Port = _rabbitSettings.Port,
            UserName = _rabbitSettings.UserName,
            Password = _rabbitSettings.Password,
            VirtualHost = _rabbitSettings.VirtualHost,
            DispatchConsumersAsync = true
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(_rabbitSettings.OrdersQueue, _rabbitSettings.Durable, false, _rabbitSettings.AutoDelete, null);
        _channel.BasicQos(0, 1, false);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += ProcessMessageAsync;
        _channel.BasicConsume(_rabbitSettings.OrdersQueue, false, consumer);

        _logger.LogInformation("Order consumer started. Queue: {Queue}", _rabbitSettings.OrdersQueue);
        try { await Task.Delay(Timeout.Infinite, stoppingToken); }
        catch (OperationCanceledException) { }
    }

    private async Task ProcessMessageAsync(object sender, BasicDeliverEventArgs args)
    {
        try
        {
            var message = JsonSerializer.Deserialize<OrderService.OrderQueueMessage>(Encoding.UTF8.GetString(args.Body.ToArray()))
                ?? throw new InvalidOperationException("Invalid order message.");

            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == message.UserId);
            if (user is null) throw new InvalidOperationException("User not found.");

            var requested = message.Products
                .GroupBy(x => x.ProductId)
                .Select(g => new { ProductId = g.Key, Count = g.Sum(x => x.Count) })
                .ToList();

            var products = await db.Products
                .Where(x => requested.Select(r => r.ProductId).Contains(x.Id))
                .ToDictionaryAsync(x => x.Id);

            var unavailable = requested
                .Where(x => !products.ContainsKey(x.ProductId) || !products[x.ProductId].IsActive || products[x.ProductId].StockQty < x.Count)
                .ToList();

            if (unavailable.Count > 0)
            {
                try
                {
                    await emailService.SendOrderWaitingEmailAsync(user.Email, unavailable.Select(x =>
                        products.TryGetValue(x.ProductId, out var product)
                            ? $"{product.Name} (requested: {x.Count}, available: {product.StockQty})"
                            : $"Product #{x.ProductId} (not available)").ToList());
                }
                catch (Exception emailException)
                {
                    _logger.LogError(emailException, "Could not send order waiting email to {Email}.", user.Email);
                }

                _channel!.BasicAck(args.DeliveryTag, false);
                return;
            }

            await using var transaction = await db.Database.BeginTransactionAsync(IsolationLevel.Serializable);
            var order = new Models.Order
            {
                UserId = user.Id,
                Paid = message.Paid,
                Status = "Confirmed",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            decimal total = 0;
            foreach (var item in requested)
            {
                var product = products[item.ProductId];
                product.StockQty -= item.Count;
                var detail = new Models.OrderDetail
                {
                    ProductId = product.Id,
                    Price = product.Price,
                    Count = item.Count
                };
                total += product.Price * item.Count;
                order.Details.Add(detail);
            }

            db.Orders.Add(order);
            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            var emailItems = order.Details.Select(d =>
            {
                var product = products[d.ProductId];
                return new OrderEmailItem(product.Name, d.Price, d.Count, d.Price * d.Count);
            }).ToList();

            try
            {
                await emailService.SendOrderConfirmedEmailAsync(user.Email, order.Id, emailItems, total);
            }
            catch (Exception emailException)
            {
                _logger.LogError(emailException, "Could not send order confirmation email for order {OrderId}.", order.Id);
            }

            _channel!.BasicAck(args.DeliveryTag, false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing order message.");
            _channel?.BasicNack(args.DeliveryTag, false, true);
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _channel?.Close();
        _connection?.Close();
        return base.StopAsync(cancellationToken);
    }
}

public sealed record OrderEmailItem(string ProductName, decimal Price, int Count, decimal Total);
