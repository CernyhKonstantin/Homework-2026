namespace HW_14._08._2026.Services.Interfaces;

public interface IEmailService
{
    Task SendPasswordResetEmailAsync(string email, string resetToken);
}
