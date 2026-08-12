using HW_10._08._2026.Models;

namespace HW_10._08._2026.Repositories.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task<RefreshToken> CreateAsync(RefreshToken token);
    Task UpdateAsync(RefreshToken token);
}
