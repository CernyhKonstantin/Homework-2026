using HW_13._07._2026.DTOs.Auth;

namespace HW_13._07._2026.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginDto dto);

    Task<bool> RegisterAsync(RegisterDto dto);
}