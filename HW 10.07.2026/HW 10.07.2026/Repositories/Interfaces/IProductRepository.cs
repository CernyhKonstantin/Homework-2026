using HW_10._07._2026.Models;

namespace HW_10._07._2026.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetByCategoryIdAsync(int categoryId);
}