using HW_12._09._2026.Data;
using HW_12._09._2026.Models;
using HW_12._09._2026.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HW_12._09._2026.Repositories;

public class UserProviderRepository : IUserProviderRepository
{
    private readonly ShopDbContext _context;

    public UserProviderRepository(ShopDbContext context) => _context = context;

    public Task<UserProvider?> GetAsync(int userId, int providerId, CancellationToken cancellationToken = default) =>
        _context.UserProviders
            .Include(x => x.Provider)
            .FirstOrDefaultAsync(x => x.UserId == userId && x.ProviderId == providerId, cancellationToken);

    public Task<UserProvider?> GetByProviderNumberAsync(
        int providerId,
        string numberProvider,
        CancellationToken cancellationToken = default) =>
        _context.UserProviders
            .FirstOrDefaultAsync(
                x => x.ProviderId == providerId && x.NumberProvider == numberProvider,
                cancellationToken);

    public async Task<UserProvider> CreateAsync(
        UserProvider userProvider,
        CancellationToken cancellationToken = default)
    {
        _context.UserProviders.Add(userProvider);
        await _context.SaveChangesAsync(cancellationToken);
        return userProvider;
    }
}
