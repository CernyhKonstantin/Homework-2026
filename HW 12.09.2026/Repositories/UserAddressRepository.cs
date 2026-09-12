using HW_12._09._2026.Data;
using HW_12._09._2026.Models;
using HW_12._09._2026.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HW_12._09._2026.Repositories;

public class UserAddressRepository : IUserAddressRepository
{
    private readonly ShopDbContext _context;

    public UserAddressRepository(ShopDbContext context) => _context = context;

    public Task<List<UserAddress>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default) =>
        _context.UserAddresses
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

    public Task<UserAddress?> GetByIdAsync(int userId, int addressId, CancellationToken cancellationToken = default) =>
        _context.UserAddresses
            .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == addressId, cancellationToken);

    public async Task<UserAddress> CreateAsync(UserAddress address, CancellationToken cancellationToken = default)
    {
        _context.UserAddresses.Add(address);
        await _context.SaveChangesAsync(cancellationToken);
        return address;
    }

    public async Task<bool> DeleteAsync(UserAddress address, CancellationToken cancellationToken = default)
    {
        _context.UserAddresses.Remove(address);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
