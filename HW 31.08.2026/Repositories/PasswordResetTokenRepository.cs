using HW_31._08._2026.Data;
using HW_31._08._2026.Models;
using HW_31._08._2026.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HW_31._08._2026.Repositories;

public class PasswordResetTokenRepository : IPasswordResetTokenRepository
{
    private readonly ShopDbContext _context;

    public PasswordResetTokenRepository(ShopDbContext context) => _context = context;

    public Task<PasswordResetToken?> GetByTokenAsync(string token) =>
        _context.PasswordResetTokens.Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == token);

    public async Task<PasswordResetToken> CreateAsync(PasswordResetToken token)
    {
        _context.PasswordResetTokens.Add(token);
        await _context.SaveChangesAsync();
        return token;
    }

    public async Task UpdateAsync(PasswordResetToken token)
    {
        _context.PasswordResetTokens.Update(token);
        await _context.SaveChangesAsync();
    }
}
