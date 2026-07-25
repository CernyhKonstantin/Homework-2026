using HW_15._07._2026.DTOs.Auth;
using HW_15._07._2026.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HW_15._07._2026.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto, Response);

        if (result == null)
        {
            return BadRequest(new
            {
                Message = "User already exists."
            });
        }

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto, Response);

        if (result == null)
        {
            return Unauthorized(new
            {
                Message = "Invalid email or password."
            });
        }

        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var result = await _authService.RefreshAsync(Request, Response);

        if (result == null)
        {
            return Unauthorized(new
            {
                Message = "Refresh token is invalid or expired."
            });
        }

        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync(Request, Response);

        return Ok(new
        {
            Message = "Logged out successfully."
        });
    }
}