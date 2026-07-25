using HW_15._07._2026.Data;
using HW_15._07._2026.Models;
using HW_15._07._2026.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HW_15._07._2026.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ShopDbContext _context;

    public RefreshTokenRepository(ShopDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByIdAsync(int id)
    {
        return await _context.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == token);
    }

    public async Task<List<RefreshToken>> GetUserTokensAsync(int userId)
    {
        return await _context.RefreshTokens
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<RefreshToken> CreateAsync(RefreshToken token)
    {
        _context.RefreshTokens.Add(token);

        await _context.SaveChangesAsync();

        return token;
    }

    public async Task UpdateAsync(RefreshToken token)
    {
        _context.RefreshTokens.Update(token);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(RefreshToken token)
    {
        _context.RefreshTokens.Remove(token);

        await _context.SaveChangesAsync();
    }
}