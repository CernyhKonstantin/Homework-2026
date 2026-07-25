using HW_06._07._2026.Models;

namespace HW_06._07._2026.Repositories.Interfaces;

public interface IProductRepository
{
    Task<Product> CreateAsync(Product product);
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
}