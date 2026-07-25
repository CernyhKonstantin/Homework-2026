using HW_15._07._2026.DTOs.Auth;
using HW_15._07._2026.Models;
using HW_15._07._2026.Repositories.Interfaces;
using HW_15._07._2026.Services.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HW_15._07._2026.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    private readonly IRefreshTokenRepository _refreshTokenRepository;

    private readonly IConfiguration _configuration;

    public AuthService(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _configuration = configuration;
    }

    private string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("UserId", user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var expires = DateTime.UtcNow.AddMinutes(
            Convert.ToDouble(
                _configuration["Jwt:AccessTokenMinutes"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }

    private RefreshToken GenerateRefreshToken(int userId)
    {
        var token = Convert.ToBase64String(
            RandomNumberGenerator.GetBytes(64));

        return new RefreshToken
        {
            Token = token,
            UserId = userId,
            IsRevoked = false,
            ExpiresAt = DateTime.UtcNow.AddDays(
                Convert.ToDouble(
                    _configuration["Jwt:RefreshTokenDays"]))
        };
    }

    private void SetRefreshTokenCookie(
        HttpResponse response,
        RefreshToken refreshToken)
    {
        response.Cookies.Append(
            "refresh_token",
            refreshToken.Token,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = refreshToken.ExpiresAt
            });
    }

    public async Task<LoginResponseDto?> RegisterAsync(
    RegisterDto dto,
    HttpResponse response)
    {
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);

        if (existingUser != null)
        {
            return null;
        }

        var user = new User
        {
            Email = dto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.CreateAsync(user);

        var createdUser = await _userRepository.GetByEmailAsync(dto.Email);

        if (createdUser == null)
        {
            return null;
        }

        var refreshToken = GenerateRefreshToken(createdUser.Id);

        await _refreshTokenRepository.CreateAsync(refreshToken);

        SetRefreshTokenCookie(response, refreshToken);

        var accessToken = GenerateAccessToken(createdUser);

        return new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            AccessTokenExpires = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:AccessTokenMinutes"])),
            RefreshTokenExpires = refreshToken.ExpiresAt
        };
    }

    public async Task<LoginResponseDto?> LoginAsync(
    LoginDto dto,
    HttpResponse response)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);

        if (user == null)
        {
            return null;
        }

        var passwordValid = BCrypt.Net.BCrypt.Verify(
            dto.Password,
            user.Password);

        if (!passwordValid)
        {
            return null;
        }

        var userTokens = await _refreshTokenRepository
            .GetUserTokensAsync(user.Id);

        foreach (var token in userTokens)
        {
            if (!token.IsRevoked)
            {
                token.IsRevoked = true;
                await _refreshTokenRepository.UpdateAsync(token);
            }
        }

        var refreshToken = GenerateRefreshToken(user.Id);

        await _refreshTokenRepository.CreateAsync(refreshToken);

        SetRefreshTokenCookie(
            response,
            refreshToken);

        var accessToken = GenerateAccessToken(user);

        return new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            AccessTokenExpires = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(
                    _configuration["Jwt:AccessTokenMinutes"])),
            RefreshTokenExpires = refreshToken.ExpiresAt
        };
    }

    public async Task<LoginResponseDto?> RefreshAsync(
    HttpRequest request,
    HttpResponse response)
    {
        if (!request.Cookies.TryGetValue("refresh_token", out var token))
        {
            return null;
        }

        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(token);

        if (refreshToken == null)
        {
            return null;
        }

        if (refreshToken.IsRevoked)
        {
            return null;
        }

        if (refreshToken.ExpiresAt <= DateTime.UtcNow)
        {
            return null;
        }

        var user = await _userRepository.GetByIdAsync(refreshToken.UserId);

        if (user == null)
        {
            return null;
        }

        refreshToken.IsRevoked = true;
        await _refreshTokenRepository.UpdateAsync(refreshToken);

        var newRefreshToken = GenerateRefreshToken(user.Id);

        await _refreshTokenRepository.CreateAsync(newRefreshToken);

        SetRefreshTokenCookie(response, newRefreshToken);

        var accessToken = GenerateAccessToken(user);

        return new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken.Token,
            AccessTokenExpires = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:AccessTokenMinutes"])),
            RefreshTokenExpires = newRefreshToken.ExpiresAt
        };
    }

    public async Task LogoutAsync(
    HttpRequest request,
    HttpResponse response)
    {
        if (request.Cookies.TryGetValue("refresh_token", out var token))
        {
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(token);

            if (refreshToken != null)
            {
                refreshToken.IsRevoked = true;

                await _refreshTokenRepository.UpdateAsync(refreshToken);
            }
        }

        response.Cookies.Delete("refresh_token");
    }
}