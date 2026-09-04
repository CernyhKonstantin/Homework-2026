using System.Net;
using System.Net.Mail;
using HW_31._08._2026.Services.Interfaces;

namespace HW_31._08._2026.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendOrderConfirmedEmailAsync(string email, int orderId, IReadOnlyCollection<OrderEmailItem> items, decimal total)
    {
        var lines = items.Select(x => $"- {x.ProductName}: {x.Count} x {x.Price:0.00} = {x.Total:0.00}");
        var body = $"Hello,\n\nYour order #{orderId} has been confirmed.\n\n" +
                   string.Join("\n", lines) +
                   $"\n\nTotal: {total:0.00}\n\nThank you for your order.";
        await SendAsync(email, $"Order #{orderId} confirmed", body);
    }

    public async Task SendOrderWaitingEmailAsync(string email, IReadOnlyCollection<string> unavailableItems)
    {
        var body = "Hello,\n\nYour order is waiting because the following products are currently unavailable or do not have enough stock:\n\n" +
                   string.Join("\n", unavailableItems.Select(x => $"- {x}")) +
                   "\n\nWe will process the order when the required stock is available.";
        await SendAsync(email, "Order waiting for stock", body);
    }

    private async Task SendAsync(string email, string subject, string body)
    {
        var host = _configuration["Email:Host"];
        var username = _configuration["Email:Username"];
        var password = _configuration["Email:Password"];
        var fromEmail = _configuration["Email:FromEmail"];
        var fromName = _configuration["Email:FromName"] ?? "HW 24 Shop";
        var port = _configuration.GetValue<int>("Email:Port", 587);
        var useSsl = _configuration.GetValue<bool>("Email:UseSsl", true);
        if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(fromEmail) || host.Contains("example.com", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Email SMTP settings are not configured. Update the Email section in appsettings.json.");
        using var message = new MailMessage { From = new MailAddress(fromEmail, fromName), Subject = subject, Body = body, IsBodyHtml = false };
        message.To.Add(email);
        using var client = new SmtpClient(host, port) { EnableSsl = useSsl, Credentials = new NetworkCredential(username, password) };
        await client.SendMailAsync(message);
    }

    public async Task SendPasswordResetEmailAsync(string email, string resetToken)
    {
        var host = _configuration["Email:Host"];
        var username = _configuration["Email:Username"];
        var password = _configuration["Email:Password"];
        var fromEmail = _configuration["Email:FromEmail"];
        var fromName = _configuration["Email:FromName"] ?? "HW 15 Shop";
        var port = _configuration.GetValue<int>("Email:Port", 587);
        var useSsl = _configuration.GetValue<bool>("Email:UseSsl", true);
        var frontendUrl = _configuration["PasswordReset:FrontendUrl"] ?? "https://localhost:5173";

        if (string.IsNullOrWhiteSpace(host) ||
            string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(fromEmail) ||
            host.Contains("example.com", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                "Email SMTP settings are not configured. Update the Email section in appsettings.json.");
        }

        var resetLink = $"{frontendUrl.TrimEnd('/')}/reset-password?token={Uri.EscapeDataString(resetToken)}";

        using var message = new MailMessage
        {
            From = new MailAddress(fromEmail, fromName),
            Subject = "Password Reset",
            Body =
                $"Hello,\n\n" +
                $"A password reset was requested for your account.\n\n" +
                $"Open this link to set a new password:\n{resetLink}\n\n" +
                $"This link expires after {_configuration.GetValue<double>("PasswordReset:TokenMinutes", 30)} minutes.\n\n" +
                $"If you did not request this, you can ignore this email.",
            IsBodyHtml = false
        };

        message.To.Add(email);

        using var client = new SmtpClient(host, port)
        {
            EnableSsl = useSsl,
            Credentials = new NetworkCredential(username, password)
        };

        await client.SendMailAsync(message);
    }
}
