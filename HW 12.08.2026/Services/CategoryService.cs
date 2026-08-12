using HW_10._08._2026.DTOs.Category;
using HW_10._08._2026.Models;
using HW_10._08._2026.Repositories.Interfaces;
using HW_10._08._2026.Services.Interfaces;

namespace HW_10._08._2026.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository) => _repository = repository;

    public async Task<List<CategoryReadDto>> GetAllAsync() =>
        (await _repository.GetAllAsync()).Select(Map).ToList();

    public async Task<CategoryReadDto?> GetByIdAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        return item == null ? null : Map(item);
    }

    public async Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto)
    {
        if (dto.ParentId.HasValue && await _repository.GetByIdAsync(dto.ParentId.Value) == null)
            throw new ArgumentException("Parent category not found.");

        var category = new Category
        {
            Name = dto.Name,
            Slug = dto.Slug,
            ParentId = dto.ParentId
        };

        return Map(await _repository.CreateAsync(category));
    }

    public async Task<CategoryReadDto?> UpdateAsync(int id, CategoryCreateDto dto)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null) return null;

        if (dto.ParentId == id)
            throw new ArgumentException("A category cannot be its own parent.");

        if (dto.ParentId.HasValue && await _repository.GetByIdAsync(dto.ParentId.Value) == null)
            throw new ArgumentException("Parent category not found.");

        category.Name = dto.Name;
        category.Slug = dto.Slug;
        category.ParentId = dto.ParentId;
        category.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(category);
        return Map(category);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null) return false;

        await _repository.DeleteAsync(category);
        return true;
    }

    public async Task<List<CategoryReadDto>> GetRootCategoriesAsync()
    {
        var all = await _repository.GetAllAsync();
        return all.Where(x => x.ParentId == null).Select(Map).ToList();
    }

    public async Task<List<CategoryReadDto>> GetChildrenAsync(int parentId)
    {
        var all = await _repository.GetAllAsync();
        return all.Where(x => x.ParentId == parentId).Select(Map).ToList();
    }

    private static CategoryReadDto Map(Category x) => new()
    {
        Id = x.Id,
        Name = x.Name,
        Slug = x.Slug,
        ParentId = x.ParentId,
        ParentName = x.Parent?.Name,
        CreatedAt = x.CreatedAt,
        UpdatedAt = x.UpdatedAt
    };
}
