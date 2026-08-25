using System.Text.Json;
using HW_24._08._2026.DTOs.Order;
using HW_24._08._2026.Helpers;
using HW_24._08._2026.Services.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace HW_24._08._2026.Services;

public class OrderService : IOrderService
{
    private readonly RabbitMqSettings _rabbitSettings;

    public OrderService(IOptions<RabbitMqSettings> rabbitOptions)
    {
        _rabbitSettings = rabbitOptions.Value;
    }

    public Task PublishOrderAsync(int userId, CreateOrderDto dto)
    {
        if (dto.Products.Count == 0)
            throw new ArgumentException("The order must contain at least one product.");

        var factory = new ConnectionFactory
        {
            HostName = _rabbitSettings.HostName,
            Port = _rabbitSettings.Port,
            UserName = _rabbitSettings.UserName,
            Password = _rabbitSettings.Password,
            VirtualHost = _rabbitSettings.VirtualHost
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(_rabbitSettings.OrdersQueue, _rabbitSettings.Durable, false, _rabbitSettings.AutoDelete, null);

        var message = new OrderQueueMessage
        {
            UserId = userId,
            Paid = dto.Paid,
            Products = dto.Products
                .GroupBy(x => x.ProductId)
                .Select(g => new OrderQueueItem { ProductId = g.Key, Count = g.Sum(x => x.Count) })
                .ToList()
        };

        var body = JsonSerializer.SerializeToUtf8Bytes(message);
        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;
        properties.ContentType = "application/json";

        channel.BasicPublish(string.Empty, _rabbitSettings.OrdersQueue, properties, body);
        return Task.CompletedTask;
    }

    public sealed class OrderQueueMessage
    {
        public int UserId { get; set; }
        public bool Paid { get; set; }
        public List<OrderQueueItem> Products { get; set; } = new();
    }

    public sealed class OrderQueueItem
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
