using HW_14._09._2026.Models;

namespace HW_14._09._2026.Repositories.Interfaces;

public interface IUserProviderRepository
{
    Task<UserProvider?> GetAsync(int userId, int providerId, CancellationToken cancellationToken = default);
    Task<UserProvider?> GetByProviderNumberAsync(int providerId, string numberProvider, CancellationToken cancellationToken = default);
    Task<UserProvider> CreateAsync(UserProvider userProvider, CancellationToken cancellationToken = default);
}
