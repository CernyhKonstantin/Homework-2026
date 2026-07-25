using HW_13._07._2026.DTOs.Auth;
using HW_13._07._2026.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HW_13._07._2026.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _service.RegisterAsync(dto);

        if (!result)
            return BadRequest("User already exists.");

        return Ok("User created.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await _service.LoginAsync(dto);

        if (token == null)
            return Unauthorized();

        return Ok(token);
    }
}