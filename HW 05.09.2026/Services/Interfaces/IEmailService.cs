using HW_05._09._2026.Services;

namespace HW_05._09._2026.Services.Interfaces;

public interface IEmailService
{
    Task SendPasswordResetEmailAsync(string email, string resetToken);
    Task SendOrderConfirmedEmailAsync(string email, int orderId, IReadOnlyCollection<OrderEmailItem> items, decimal total);
    Task SendOrderWaitingEmailAsync(string email, IReadOnlyCollection<string> unavailableItems);
}
