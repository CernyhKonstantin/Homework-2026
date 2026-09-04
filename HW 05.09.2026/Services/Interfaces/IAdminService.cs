using HW_05._09._2026.DTOs.Auth;
using HW_05._09._2026.Models;

namespace HW_05._09._2026.Services.Interfaces;

public interface IAdminService
{
    Task<User> CreateUserAsync(CreateUserDto dto);
    Task<bool> ChangeRoleAsync(int userId, string role);
    Task<List<User>> GetUsersAsync();
}
