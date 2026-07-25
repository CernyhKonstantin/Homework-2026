using HW_10._07._2026.DTOs.Category;
using HW_10._07._2026.Models;
using HW_10._07._2026.Repositories.Interfaces;
using HW_10._07._2026.Services.Interfaces;

namespace HW_10._07._2026.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CategoryReadDto>> GetRootCategoriesAsync()
    {
        var categories = await _repository.GetRootCategoriesAsync();

        return categories.Select(MapCategory).ToList();
    }

    public async Task<List<CategoryReadDto>> GetChildrenAsync(int parentId)
    {
        var categories = await _repository.GetChildrenAsync(parentId);

        return categories.Select(MapCategory).ToList();
    }

    public async Task<List<CategorySearchDto>> SearchAsync(string name)
    {
        var categories = await _repository.SearchAsync(name);

        return categories.Select(c => new CategorySearchDto
        {
            Id = c.Id,
            Name = c.Name
        }).ToList();
    }

    public async Task<List<CategoryTreeDto>> GetTreeAsync()
    {
        var categories = await _repository.GetAllAsync();

        return BuildTree(categories, null);
    }

    private List<CategoryTreeDto> BuildTree(List<Category> categories, int? parentId)
    {
        return categories
            .Where(c => c.ParentId == parentId)
            .Select(c => new CategoryTreeDto
            {
                Id = c.Id,
                Name = c.Name,
                Children = BuildTree(categories, c.Id)
            })
            .ToList();
    }

    private CategoryReadDto MapCategory(Category category)
    {
        return new CategoryReadDto
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            ParentId = category.ParentId
        };
    }
}