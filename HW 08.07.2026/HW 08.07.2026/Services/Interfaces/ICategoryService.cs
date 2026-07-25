using HW_08._07._2026.DTOs.Category;

namespace HW_08._07._2026.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryReadDto>> GetAllAsync();
        Task<CategoryReadDto?> GetByIdAsync(int id);
        Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto);
        Task<bool> UpdateAsync(int id, CategoryUpdateDto dto);
        Task<bool> DeleteAsync(int id);

        Task<List<CategoryReadDto>> GetParentsByCategoryIdAsync(int categoryId);
        Task<List<CategoryReadDto>> GetChildrenByCategoryIdAsync(int categoryId);
        Task<List<CategoryTreeDto>> GetCategoryTreeAsync();
    }
}