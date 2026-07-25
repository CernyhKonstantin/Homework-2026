using HW_06._07._2026.DTOs.Category;
using HW_06._07._2026.Models;
using HW_06._07._2026.Repositories.Interfaces;
using HW_06._07._2026.Services.Interfaces;

namespace HW_06._07._2026.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            Slug = dto.Slug,
            ParentId = dto.ParentId
        };

        var createdCategory = await _categoryRepository.CreateAsync(category);

        return new CategoryReadDto
        {
            Id = createdCategory.Id,
            Name = createdCategory.Name,
            Slug = createdCategory.Slug,
            ParentId = createdCategory.ParentId
        };
    }

    public async Task<List<CategoryReadDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();

        return categories.Select(c => new CategoryReadDto
        {
            Id = c.Id,
            Name = c.Name,
            Slug = c.Slug,
            ParentId = c.ParentId
        }).ToList();
    }

    public async Task<CategoryReadDto?> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
        {
            return null;
        }

        return new CategoryReadDto
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            ParentId = category.ParentId
        };
    }
}