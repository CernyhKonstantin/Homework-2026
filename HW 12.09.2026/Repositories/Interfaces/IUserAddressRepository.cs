using HW_12._09._2026.Models;

namespace HW_12._09._2026.Repositories.Interfaces;

public interface IUserAddressRepository
{
    Task<List<UserAddress>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<UserAddress?> GetByIdAsync(int userId, int addressId, CancellationToken cancellationToken = default);
    Task<UserAddress> CreateAsync(UserAddress address, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(UserAddress address, CancellationToken cancellationToken = default);
}
