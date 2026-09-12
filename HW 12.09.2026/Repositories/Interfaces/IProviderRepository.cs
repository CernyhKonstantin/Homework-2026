using HW_12._09._2026.Models;

namespace HW_12._09._2026.Repositories.Interfaces;

public interface IProviderRepository
{
    Task<Provider?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<List<Provider>> GetAllAsync(CancellationToken cancellationToken = default);
}
