using HW_14._09._2026.Services.Interfaces;
using HW_14._09._2026.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HW_14._09._2026.Controllers;

public sealed class UsersController : Controller
{
    private readonly IAdminService _adminService;

    public UsersController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _adminService.GetUsersAsync();

        var model = users
            .Select(user => new UserListItemViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            })
            .ToList();

        return View(model);
    }
}
