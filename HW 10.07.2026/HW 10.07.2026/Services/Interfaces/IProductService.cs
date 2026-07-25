using HW_10._07._2026.DTOs.Product;

namespace HW_10._07._2026.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductReadDto>> GetByCategoryIdAsync(int categoryId);
}