using HW_09._09._2026.DTOs.Address;
using HW_09._09._2026.Models;
using HW_09._09._2026.Repositories.Interfaces;
using HW_09._09._2026.Services.Interfaces;

namespace HW_09._09._2026.Services;

public class UserAddressService : IUserAddressService
{
    private readonly IUserAddressRepository _repository;

    public UserAddressService(IUserAddressRepository repository) => _repository = repository;

    public async Task<List<UserAddressReadDto>> GetMyAddressesAsync(int userId, CancellationToken cancellationToken = default)
    {
        var addresses = await _repository.GetByUserIdAsync(userId, cancellationToken);
        return addresses.Select(Map).ToList();
    }

    public async Task<UserAddressReadDto?> GetMyAddressAsync(int userId, int addressId, CancellationToken cancellationToken = default)
    {
        var address = await _repository.GetByIdAsync(userId, addressId, cancellationToken);
        return address is null ? null : Map(address);
    }

    public async Task<UserAddressReadDto> CreateAsync(int userId, CreateUserAddressDto dto, CancellationToken cancellationToken = default)
    {
        var address = new UserAddress
        {
            UserId = userId,
            Label = dto.Label.Trim(),
            RecipientName = dto.RecipientName.Trim(),
            Country = dto.Country.Trim(),
            City = dto.City.Trim(),
            PostalCode = dto.PostalCode.Trim(),
            Street = dto.Street.Trim(),
            HouseNumber = dto.HouseNumber.Trim(),
            Apartment = dto.Apartment?.Trim() ?? string.Empty,
            Phone = dto.Phone?.Trim() ?? string.Empty,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _repository.CreateAsync(address, cancellationToken);
        return Map(created);
    }

    public async Task<bool> DeleteAsync(int userId, int addressId, CancellationToken cancellationToken = default)
    {
        var address = await _repository.GetByIdAsync(userId, addressId, cancellationToken);
        if (address is null)
            return false;

        return await _repository.DeleteAsync(address, cancellationToken);
    }

    private static UserAddressReadDto Map(UserAddress address) => new()
    {
        Id = address.Id,
        Label = address.Label,
        RecipientName = address.RecipientName,
        Country = address.Country,
        City = address.City,
        PostalCode = address.PostalCode,
        Street = address.Street,
        HouseNumber = address.HouseNumber,
        Apartment = address.Apartment,
        Phone = address.Phone,
        CreatedAt = address.CreatedAt
    };
}
