using HW_15._07._2026.DTOs.Auth;
using Microsoft.AspNetCore.Http;

namespace HW_15._07._2026.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto?> RegisterAsync(
        RegisterDto dto,
        HttpResponse response);

    Task<LoginResponseDto?> LoginAsync(
        LoginDto dto,
        HttpResponse response);

    Task<LoginResponseDto?> RefreshAsync(
        HttpRequest request,
        HttpResponse response);

    Task LogoutAsync(
        HttpRequest request,
        HttpResponse response);
}