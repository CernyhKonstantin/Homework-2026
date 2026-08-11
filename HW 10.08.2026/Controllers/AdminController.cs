using HW_10._08._2026.DTOs.Auth;
using HW_10._08._2026.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HW_10._08._2026.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _admin;

    public AdminController(IAdminService admin) => _admin = admin;

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _admin.GetUsersAsync();
        return Ok(users.Select(x => new
        {
            x.Id,
            x.Email,
            x.Role,
            x.CreatedAt
        }));
    }

    [HttpPost("users")]
    public async Task<IActionResult> CreateUser(CreateUserDto dto)
    {
        try
        {
            var user = await _admin.CreateUserAsync(dto);
            return Ok(new
            {
                user.Id,
                user.Email,
                user.Role,
                user.CreatedAt
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPut("users/{id}/role")]
    public async Task<IActionResult> ChangeRole(int id, ChangeRoleDto dto)
    {
        var success = await _admin.ChangeRoleAsync(id, dto.Role);
        return success ? Ok(new { message = "Role updated." }) : BadRequest("Invalid user or role.");
    }
}
