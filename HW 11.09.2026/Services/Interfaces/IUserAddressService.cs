using HW_11._09._2026.DTOs.Address;

namespace HW_11._09._2026.Services.Interfaces;

public interface IUserAddressService
{
    Task<List<UserAddressReadDto>> GetMyAddressesAsync(int userId, CancellationToken cancellationToken = default);
    Task<UserAddressReadDto?> GetMyAddressAsync(int userId, int addressId, CancellationToken cancellationToken = default);
    Task<UserAddressReadDto> CreateAsync(int userId, CreateUserAddressDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int userId, int addressId, CancellationToken cancellationToken = default);
}
