using HW_05._09._2026.DTOs.Product;

namespace HW_05._09._2026.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductReadDto>> GetAllAsync();
    Task<ProductReadDto?> GetByIdAsync(int id);
    Task<List<ProductReadDto>> GetByCategoryIdAsync(int categoryId);
    Task<ProductReadDto> CreateAsync(ProductCreateDto dto);
    Task<ProductReadDto?> UpdateAsync(int id, ProductCreateDto dto);
    Task<bool> DeleteAsync(int id);
}
