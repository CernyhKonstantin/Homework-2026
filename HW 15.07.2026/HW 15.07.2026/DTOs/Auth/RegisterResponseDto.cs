namespace HW_15._07._2026.DTOs.Auth;

public class RegisterResponseDto
{
    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;

    public DateTime AccessTokenExpires { get; set; }

    public DateTime RefreshTokenExpires { get; set; }

    public string TokenType { get; set; } = "Bearer";
}