using HW_06._07._2026.Models;

namespace HW_06._07._2026.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<Category> CreateAsync(Category category);
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
}