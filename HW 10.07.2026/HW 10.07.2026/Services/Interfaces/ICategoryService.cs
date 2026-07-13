using HW_10._07._2026.DTOs.Category;

namespace HW_10._07._2026.Services.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryReadDto>> GetRootCategoriesAsync();

    Task<List<CategoryReadDto>> GetChildrenAsync(int parentId);

    Task<List<CategorySearchDto>> SearchAsync(string name);

    Task<List<CategoryTreeDto>> GetTreeAsync();
}