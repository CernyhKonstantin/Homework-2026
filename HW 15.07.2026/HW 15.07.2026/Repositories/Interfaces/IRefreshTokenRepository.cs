using HW_15._07._2026.Models;

namespace HW_15._07._2026.Repositories.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByIdAsync(int id);

    Task<RefreshToken?> GetByTokenAsync(string token);

    Task<List<RefreshToken>> GetUserTokensAsync(int userId);

    Task<RefreshToken> CreateAsync(RefreshToken token);

    Task UpdateAsync(RefreshToken token);

    Task DeleteAsync(RefreshToken token);
}