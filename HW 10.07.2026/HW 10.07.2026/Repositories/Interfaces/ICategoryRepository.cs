using HW_10._07._2026.Models;

namespace HW_10._07._2026.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();

    Task<Category?> GetByIdAsync(int id);

    Task<List<Category>> GetRootCategoriesAsync();

    Task<List<Category>> GetChildrenAsync(int parentId);

    Task<List<Category>> SearchAsync(string name);
}