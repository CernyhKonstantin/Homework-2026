using HW_10._08._2026.Data;
using HW_10._08._2026.Models;
using HW_10._08._2026.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HW_10._08._2026.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ShopDbContext _context;

    public UserRepository(ShopDbContext context) => _context = context;

    public Task<List<User>> GetAllAsync() =>
        _context.Users.AsNoTracking().ToListAsync();

    public Task<User?> GetByIdAsync(int id) =>
        _context.Users.FirstOrDefaultAsync(x => x.Id == id);

    public Task<User?> GetByEmailAsync(string email) =>
        _context.Users.FirstOrDefaultAsync(x => x.Email == email);

    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}
