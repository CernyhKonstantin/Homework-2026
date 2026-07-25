using HW_03_07_2026.DTOs.Category;
using HW_03_07_2026.Models;
using HW_03_07_2026.Repositories.Interfaces;
using HW_03_07_2026.Services.Interfaces;

namespace HW_03_07_2026.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repo;

    public CategoryService(ICategoryRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<CategoryReadDto>> GetAllAsync()
    {
        var data = await _repo.GetAllAsync();

        return data.Select(x => new CategoryReadDto
        {
            Id = x.Id,
            Name = x.Name,
            Slug = x.Slug,
            ParentId = x.ParentId
        }).ToList();
    }

    public async Task<CategoryReadDto?> GetByIdAsync(int id)
    {
        var x = await _repo.GetByIdAsync(id);
        if (x == null) return null;

        return new CategoryReadDto
        {
            Id = x.Id,
            Name = x.Name,
            Slug = x.Slug,
            ParentId = x.ParentId
        };
    }

    public async Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto)
    {
        var entity = new Category
        {
            Name = dto.Name,
            Slug = dto.Slug,
            ParentId = dto.ParentId
        };

        var created = await _repo.CreateAsync(entity);

        return new CategoryReadDto
        {
            Id = created.Id,
            Name = created.Name,
            Slug = created.Slug,
            ParentId = created.ParentId
        };
    }

    public async Task<CategoryReadDto?> UpdateAsync(int id, CategoryUpdateDto dto)
    {
        var entity = new Category
        {
            Id = id,
            Name = dto.Name,
            Slug = dto.Slug,
            ParentId = dto.ParentId
        };

        var updated = await _repo.UpdateAsync(entity);
        if (updated == null) return null;

        return new CategoryReadDto
        {
            Id = updated.Id,
            Name = updated.Name,
            Slug = updated.Slug,
            ParentId = updated.ParentId
        };
    }

    public async Task<bool> DeleteAsync(int id)
        => await _repo.DeleteAsync(id);
}