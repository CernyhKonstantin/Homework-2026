using HW_05._09._2026.Models;

namespace HW_05._09._2026.Repositories.Interfaces;

public interface IPasswordResetTokenRepository
{
    Task<PasswordResetToken?> GetByTokenAsync(string token);
    Task<PasswordResetToken> CreateAsync(PasswordResetToken token);
    Task UpdateAsync(PasswordResetToken token);
}
