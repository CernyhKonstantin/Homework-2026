using HW_05._09._2026.DTOs.Auth;
using HW_05._09._2026.Models;
using HW_05._09._2026.Repositories.Interfaces;
using HW_05._09._2026.Services.Interfaces;

namespace HW_05._09._2026.Services;

public class AdminService : IAdminService
{
    private static readonly string[] AllowedRoles = ["User", "Moderator", "Admin"];
    private readonly IUserRepository _userRepository;

    public AdminService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateUserAsync(CreateUserDto dto)
    {
        var email = dto.Email.Trim().ToLowerInvariant();

        if (!AllowedRoles.Contains(dto.Role, StringComparer.OrdinalIgnoreCase))
            throw new ArgumentException("Invalid role.");

        if (await _userRepository.GetByEmailAsync(email) != null)
            throw new InvalidOperationException("User already exists.");

        var user = new User
        {
            Email = email,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = NormalizeRole(dto.Role),
            CreatedAt = DateTime.UtcNow
        };

        return await _userRepository.CreateAsync(user);
    }

    public async Task<bool> ChangeRoleAsync(int userId, string role)
    {
        if (!AllowedRoles.Contains(role, StringComparer.OrdinalIgnoreCase))
            return false;

        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return false;

        user.Role = NormalizeRole(role);
        await _userRepository.UpdateAsync(user);
        return true;
    }

    public Task<List<User>> GetUsersAsync() => _userRepository.GetAllAsync();

    private static string NormalizeRole(string role) =>
        char.ToUpperInvariant(role[0]) + role[1..].ToLowerInvariant();
}
