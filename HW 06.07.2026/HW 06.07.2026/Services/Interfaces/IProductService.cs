using HW_06._07._2026.DTOs.Product;

namespace HW_06._07._2026.Services.Interfaces;

public interface IProductService
{
    Task<ProductReadDto> CreateAsync(ProductCreateDto dto);
    Task<List<ProductReadDto>> GetAllAsync();
    Task<ProductReadDto?> GetByIdAsync(int id);
}