using HW_13._07._2026.Data;
using HW_13._07._2026.Models;
using HW_13._07._2026.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HW_13._07._2026.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ShopDbContext _context;

    public UserRepository(ShopDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user;
    }
}