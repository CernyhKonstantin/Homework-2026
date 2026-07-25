using HW_15._07._2026.DTOs.Category;

namespace HW_15._07._2026.Services.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryReadDto>> GetAllAsync();

    Task<CategoryReadDto?> GetByIdAsync(int id);

    Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto);

    Task<CategoryReadDto?> UpdateAsync(
        int id,
        CategoryCreateDto dto);

    Task<bool> DeleteAsync(int id);

    Task<List<CategoryReadDto>> GetRootCategoriesAsync();

    Task<List<CategoryReadDto>> GetChildrenAsync(int parentId);

    Task<List<CategoryReadDto>> GetParentsAsync(int categoryId);
}