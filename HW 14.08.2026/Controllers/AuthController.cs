using HW_14._08._2026.DTOs.Auth;
using HW_14._08._2026.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HW_14._08._2026.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;

    public AuthController(IAuthService auth) => _auth = auth;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _auth.RegisterAsync(dto, Response);
        return result == null ? BadRequest("Registration failed.") : Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _auth.LoginAsync(dto, Response);
        return result == null ? Unauthorized("Invalid email or password.") : Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var result = await _auth.RefreshAsync(Request, Response);
        return result == null ? Unauthorized("Invalid or expired refresh token.") : Ok(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _auth.LogoutAsync(Request, Response);
        return NoContent();
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
    {
        await _auth.RequestPasswordResetAsync(dto.Email);
        return Ok(new { message = "If the email exists, a reset link has been sent." });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
    {
        var success = await _auth.ResetPasswordAsync(dto);
        return success ? Ok(new { message = "Password reset successfully." })
                       : BadRequest("Invalid or expired reset token.");
    }
}
