namespace HW_31._08._2026.Models;

public class PasswordResetToken
{
    public int Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
