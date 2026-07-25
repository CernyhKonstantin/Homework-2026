using HW_15._07._2026.DTOs.Category;
using HW_15._07._2026.Models;
using HW_15._07._2026.Repositories.Interfaces;
using HW_15._07._2026.Services.Interfaces;

namespace HW_15._07._2026.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    private static CategoryReadDto MapToDto(Category category)
    {
        return new CategoryReadDto
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            ParentId = category.ParentId,
            ParentName = category.Parent?.Name,
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt
        };
    }

    private static List<CategoryReadDto> MapToDtoList(IEnumerable<Category> categories)
    {
        return categories
            .Select(MapToDto)
            .ToList();
    }

    public async Task<List<CategoryReadDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();

        return MapToDtoList(categories);
    }

    public async Task<CategoryReadDto?> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
        {
            return null;
        }

        return MapToDto(category);
    }

    public async Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            Slug = dto.Slug,
            ParentId = dto.ParentId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        category = await _categoryRepository.CreateAsync(category);

        return MapToDto(category);
    }

    public async Task<CategoryReadDto?> UpdateAsync(
        int id,
        CategoryCreateDto dto)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
        {
            return null;
        }

        category.Name = dto.Name;
        category.Slug = dto.Slug;
        category.ParentId = dto.ParentId;
        category.UpdatedAt = DateTime.UtcNow;

        await _categoryRepository.UpdateAsync(category);

        return MapToDto(category);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
        {
            return false;
        }

        await _categoryRepository.DeleteAsync(category);

        return true;
    }

    public async Task<List<CategoryReadDto>> GetRootCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();

        var rootCategories = categories
            .Where(c => c.ParentId == null)
            .ToList();

        return MapToDtoList(rootCategories);
    }

    public async Task<List<CategoryReadDto>> GetChildrenAsync(int parentId)
    {
        var categories = await _categoryRepository.GetAllAsync();

        var children = categories
            .Where(c => c.ParentId == parentId)
            .ToList();

        return MapToDtoList(children);
    }

    public async Task<List<CategoryReadDto>> GetParentsAsync(int categoryId)
    {
        var categories = await _categoryRepository.GetAllAsync();

        var result = new List<CategoryReadDto>();

        var current = categories.FirstOrDefault(c => c.Id == categoryId);

        while (current != null && current.ParentId != null)
        {
            current = categories.FirstOrDefault(c => c.Id == current.ParentId);

            if (current != null)
            {
                result.Add(MapToDto(current));
            }
        }

        result.Reverse();

        return result;
    }
}