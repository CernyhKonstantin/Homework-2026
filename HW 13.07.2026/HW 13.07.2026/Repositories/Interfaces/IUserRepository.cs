using HW_13._07._2026.Models;

namespace HW_13._07._2026.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);

    Task<User> CreateAsync(User user);
}