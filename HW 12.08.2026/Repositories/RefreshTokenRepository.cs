using HW_10._08._2026.Data;
using HW_10._08._2026.Models;
using HW_10._08._2026.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HW_10._08._2026.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ShopDbContext _context;

    public RefreshTokenRepository(ShopDbContext context) => _context = context;

    public Task<RefreshToken?> GetByTokenAsync(string token) =>
        _context.RefreshTokens.Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == token);

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
}
