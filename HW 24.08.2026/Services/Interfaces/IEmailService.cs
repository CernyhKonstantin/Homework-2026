using HW_24._08._2026.Services;

namespace HW_24._08._2026.Services.Interfaces;

public interface IEmailService
{
    Task SendPasswordResetEmailAsync(string email, string resetToken);
    Task SendOrderConfirmedEmailAsync(string email, int orderId, IReadOnlyCollection<OrderEmailItem> items, decimal total);
    Task SendOrderWaitingEmailAsync(string email, IReadOnlyCollection<string> unavailableItems);
}
