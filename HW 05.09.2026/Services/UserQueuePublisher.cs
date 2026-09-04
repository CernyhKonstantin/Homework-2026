using System.Text.Json;
using HW_05._09._2026.Helpers;
using HW_05._09._2026.Services.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace HW_05._09._2026.Services;

public sealed class UserQueuePublisher : IUserQueuePublisher
{
    private readonly RabbitMqSettings _settings;

    public UserQueuePublisher(IOptions<RabbitMqSettings> options)
    {
        _settings = options.Value;
    }

    public Task PublishUserRegisteredAsync(int userId, string email)
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.HostName,
            Port = _settings.Port,
            UserName = _settings.UserName,
            Password = _settings.Password,
            VirtualHost = _settings.VirtualHost
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: _settings.UserQueue,
            durable: _settings.Durable,
            exclusive: false,
            autoDelete: _settings.AutoDelete,
            arguments: null);

        var payload = new
        {
            UserId = userId,
            Email = email,
            RegisteredAtUtc = DateTime.UtcNow
        };

        var body = JsonSerializer.SerializeToUtf8Bytes(payload);

        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;
        properties.ContentType = "application/json";

        channel.BasicPublish(
            exchange: string.Empty,
            routingKey: _settings.UserQueue,
            basicProperties: properties,
            body: body);

        return Task.CompletedTask;
    }
}
