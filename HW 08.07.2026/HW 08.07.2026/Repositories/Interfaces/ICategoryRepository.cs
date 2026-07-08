using HW_08._07._2026.Models;

namespace HW_08._07._2026.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);

        Task<List<Category>> GetParentsByCategoryIdAsync(int categoryId);
        Task<List<Category>> GetChildrenByCategoryIdAsync(int categoryId);
    }
}