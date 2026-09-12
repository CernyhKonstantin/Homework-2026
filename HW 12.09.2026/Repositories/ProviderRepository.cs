using HW_12._09._2026.Data;
using HW_12._09._2026.Models;
using HW_12._09._2026.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HW_12._09._2026.Repositories;

public class ProviderRepository : IProviderRepository
{
    private readonly ShopDbContext _context;

    public ProviderRepository(ShopDbContext context) => _context = context;

    public Task<Provider?> GetByNameAsync(string name, CancellationToken cancellationToken = default) =>
        _context.Providers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

    public Task<List<Provider>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _context.Providers
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);
}
