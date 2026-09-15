using HW_14._09._2026.Models;

namespace HW_14._09._2026.Repositories.Interfaces;

public interface IProviderRepository
{
    Task<Provider?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<List<Provider>> GetAllAsync(CancellationToken cancellationToken = default);
}
