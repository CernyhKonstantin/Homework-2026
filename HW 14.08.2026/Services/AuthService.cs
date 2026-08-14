using HW_14._08._2026.DTOs.Auth;
using HW_14._08._2026.Helpers;
using HW_14._08._2026.Models;
using HW_14._08._2026.Repositories.Interfaces;
using HW_14._08._2026.Services.Interfaces;

namespace HW_14._08._2026.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
    private readonly JwtTokenGenerator _jwt;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;

    public AuthService(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IPasswordResetTokenRepository passwordResetTokenRepository,
        JwtTokenGenerator jwt,
        IEmailService emailService,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _passwordResetTokenRepository = passwordResetTokenRepository;
        _jwt = jwt;
        _emailService = emailService;
        _configuration = configuration;
    }

    public async Task<RegisterResponseDto?> RegisterAsync(RegisterDto dto, HttpResponse response)
    {
        var email = dto.Email.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(dto.Password))
            return null;

        if (dto.Password != dto.RepeatPassword || dto.Password.Length < 8)
            return null;

        if (await _userRepository.GetByEmailAsync(email) != null)
            return null;

        var user = await _userRepository.CreateAsync(new User
        {
            Email = email,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = "User",
            CreatedAt = DateTime.UtcNow
        });

        var auth = await CreateAuthResponseAsync(user, response);

        return new RegisterResponseDto
        {
            Id = user.Id,
            Email = auth.Email,
            Role = auth.Role,
            AccessToken = auth.AccessToken,
            RefreshToken = auth.RefreshToken,
            AccessTokenExpiresAt = auth.AccessTokenExpiresAt
        };
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginDto dto, HttpResponse response)
    {
        var email = dto.Email.Trim().ToLowerInvariant();
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            return null;

        return await CreateAuthResponseAsync(user, response);
    }

    public async Task<LoginResponseDto?> RefreshAsync(HttpRequest request, HttpResponse response)
    {
        var tokenValue = request.Cookies["refresh_token"];

        if (string.IsNullOrWhiteSpace(tokenValue))
            return null;

        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(tokenValue);

        if (refreshToken == null ||
            refreshToken.IsRevoked ||
            refreshToken.ExpiresAt <= DateTime.UtcNow)
            return null;

        refreshToken.IsRevoked = true;
        await _refreshTokenRepository.UpdateAsync(refreshToken);

        return await CreateAuthResponseAsync(refreshToken.User, response);
    }

    public async Task LogoutAsync(HttpRequest request, HttpResponse response)
    {
        var tokenValue = request.Cookies["refresh_token"];

        if (!string.IsNullOrWhiteSpace(tokenValue))
        {
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(tokenValue);

            if (refreshToken != null)
            {
                refreshToken.IsRevoked = true;
                await _refreshTokenRepository.UpdateAsync(refreshToken);
            }
        }

        response.Cookies.Delete("refresh_token");
    }

    public async Task RequestPasswordResetAsync(string email)
    {
        email = email.Trim().ToLowerInvariant();
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null)
            return;

        var token = new PasswordResetToken
        {
            Token = _jwt.GeneratePasswordResetToken(),
            UserId = user.Id,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddMinutes(
                _configuration.GetValue<double>("PasswordReset:TokenMinutes", 30)),
            IsUsed = false
        };

        await _passwordResetTokenRepository.CreateAsync(token);
        await _emailService.SendPasswordResetEmailAsync(user.Email, token.Token);
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
    {
        var resetToken = await _passwordResetTokenRepository.GetByTokenAsync(dto.Token);

        if (resetToken == null ||
            resetToken.IsUsed ||
            resetToken.ExpiresAt <= DateTime.UtcNow)
            return false;

        if (string.IsNullOrWhiteSpace(dto.NewPassword) || dto.NewPassword.Length < 8)
            return false;

        resetToken.User.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
        resetToken.IsUsed = true;

        await _userRepository.UpdateAsync(resetToken.User);
        await _passwordResetTokenRepository.UpdateAsync(resetToken);

        return true;
    }

    private async Task<LoginResponseDto> CreateAuthResponseAsync(User user, HttpResponse response)
    {
        var access = _jwt.GenerateAccessToken(user);
        var refresh = _jwt.GenerateRefreshToken(user.Id);

        await _refreshTokenRepository.CreateAsync(refresh);

        response.Cookies.Append("refresh_token", refresh.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = response.HttpContext.Request.IsHttps,
            SameSite = SameSiteMode.Strict,
            Expires = refresh.ExpiresAt
        });

        return new LoginResponseDto
        {
            AccessToken = access.Token,
            RefreshToken = refresh.Token,
            AccessTokenExpiresAt = access.ExpiresAt,
            Email = user.Email,
            Role = user.Role
        };
    }
}
