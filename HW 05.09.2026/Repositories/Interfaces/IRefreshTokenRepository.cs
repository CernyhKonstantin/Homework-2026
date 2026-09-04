using HW_05._09._2026.Models;

namespace HW_05._09._2026.Repositories.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task<RefreshToken> CreateAsync(RefreshToken token);
    Task UpdateAsync(RefreshToken token);
}
