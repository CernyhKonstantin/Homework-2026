using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var settings = configuration.GetSection("RabbitMQ").Get<RabbitMqSettings>()
               ?? throw new InvalidOperationException("RabbitMQ configuration is missing.");

var factory = new ConnectionFactory
{
    HostName = settings.HostName,
    Port = settings.Port,
    UserName = settings.UserName,
    Password = settings.Password,
    VirtualHost = settings.VirtualHost
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
    queue: settings.UserQueue,
    durable: settings.Durable,
    exclusive: false,
    autoDelete: settings.AutoDelete,
    arguments: null);

channel.BasicQos(0, 1, false);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (_, eventArgs) =>
{
    var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

    try
    {
        var user = JsonSerializer.Deserialize<UserRegisteredMessage>(message);

        if (user is null)
            throw new InvalidOperationException("Invalid user message.");

        Console.WriteLine($"Received user: {user.UserId} | {user.Email} | {user.RegisteredAtUtc:O}");

        // Acknowledging the message removes it from the queue.
        channel.BasicAck(eventArgs.DeliveryTag, multiple: false);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Message processing failed: {ex.Message}");

        // Reject and requeue failed messages so they are not lost.
        channel.BasicNack(eventArgs.DeliveryTag, multiple: false, requeue: true);
    }
};

channel.BasicConsume(
    queue: settings.UserQueue,
    autoAck: false,
    consumer: consumer);

Console.WriteLine($"RabbitMqReader is listening on queue '{settings.UserQueue}'.");
Console.WriteLine("Create users in the API and watch them appear here.");
Console.WriteLine("Press Ctrl+C to stop.");

await Task.Delay(Timeout.Infinite);

public sealed class RabbitMqSettings
{
    public string HostName { get; set; } = "localhost";
    public int Port { get; set; } = 5672;
    public string UserName { get; set; } = "guest";
    public string Password { get; set; } = "guest";
    public string VirtualHost { get; set; } = "/";
    public string UserQueue { get; set; } = "Users";
    public bool Durable { get; set; } = true;
    public bool AutoDelete { get; set; } = false;
}

public sealed class UserRegisteredMessage
{
    public int UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public DateTime RegisteredAtUtc { get; set; }
}
