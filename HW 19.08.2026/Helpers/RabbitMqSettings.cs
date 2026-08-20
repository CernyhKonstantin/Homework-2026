namespace HW_19._08._2026.Helpers;

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
