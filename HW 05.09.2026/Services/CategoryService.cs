using System.Text.Json;
using HW_05._09._2026.DTOs.Category;
using HW_05._09._2026.Models;
using HW_05._09._2026.Repositories.Interfaces;
using HW_05._09._2026.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace HW_05._09._2026.Services;

public class CategoryService : ICategoryService
{
    private const string AllCategoriesCacheKey = "categories:all";
    private static string GetCategoryCacheKey(int id) => $"categories:id:{id}";
    private static string GetRootCategoriesCacheKey() => "categories:root";
    private static string GetChildrenCacheKey(int id) => $"categories:children:{id}";

    private readonly ICategoryRepository _repository;
    private readonly IConfiguration _configuration;
    private readonly IDistributedCache _cache;

    public CategoryService(
        ICategoryRepository repository,
        IConfiguration configuration,
        IDistributedCache cache)
    {
        _repository = repository;
        _configuration = configuration;
        _cache = cache;
    }

    public async Task<List<CategoryReadDto>> GetAllAsync()
    {
        var cached = await GetCachedAsync<List<CategoryReadDto>>(AllCategoriesCacheKey);
        if (cached is not null)
            return cached;

        var result = (await _repository.GetAllAsync()).Select(Map).ToList();
        await SetCachedAsync(AllCategoriesCacheKey, result);

        return result;
    }

    public async Task<CategoryReadDto?> GetByIdAsync(int id)
    {
        var cacheKey = GetCategoryCacheKey(id);

        // 1. Always check Redis first.
        var cached = await GetCachedAsync<CategoryReadDto>(cacheKey);
        if (cached is not null)
            return cached;

        // 2. Cache miss: load the category from the database.
        var category = await _repository.GetByIdAsync(id);
        if (category is null)
            return null;

        var result = Map(category);

        // 3. Store the database result in Redis.
        await SetCachedAsync(cacheKey, result);

        return result;
    }

    public async Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto)
    {
        if (dto.ParentId.HasValue &&
            await _repository.GetByIdAsync(dto.ParentId.Value) is null)
        {
            throw new ArgumentException("Parent category not found.");
        }

        var category = new Category
        {
            Name = dto.Name,
            Slug = dto.Slug,
            ParentId = dto.ParentId
        };

        var created = await _repository.CreateAsync(category);
        await InvalidateCategoryCachesAsync(created.Id, created.ParentId);

        var result = Map(created);
        await SetCachedAsync(GetCategoryCacheKey(created.Id), result);

        return result;
    }

    public async Task<CategoryReadDto?> UpdateAsync(int id, CategoryCreateDto dto)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category is null)
            return null;

        if (dto.ParentId == id)
            throw new ArgumentException("A category cannot be its own parent.");

        if (dto.ParentId.HasValue &&
            await _repository.GetByIdAsync(dto.ParentId.Value) is null)
        {
            throw new ArgumentException("Parent category not found.");
        }

        var oldParentId = category.ParentId;

        category.Name = dto.Name;
        category.Slug = dto.Slug;
        category.ParentId = dto.ParentId;
        category.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(category);

        await InvalidateCategoryCachesAsync(id, oldParentId);
        await InvalidateCategoryCachesAsync(id, dto.ParentId);

        var result = Map(category);
        await SetCachedAsync(GetCategoryCacheKey(id), result);

        return result;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category is null)
            return false;

        await _repository.DeleteAsync(category);
        await InvalidateCategoryCachesAsync(id, category.ParentId);

        return true;
    }

    public async Task<List<CategoryReadDto>> GetRootCategoriesAsync()
    {
        var cached = await GetCachedAsync<List<CategoryReadDto>>(GetRootCategoriesCacheKey());
        if (cached is not null)
            return cached;

        var result = (await _repository.GetAllAsync())
            .Where(x => x.ParentId == null)
            .Select(Map)
            .ToList();

        await SetCachedAsync(GetRootCategoriesCacheKey(), result);
        return result;
    }

    public async Task<List<CategoryReadDto>> GetChildrenAsync(int parentId)
    {
        var cacheKey = GetChildrenCacheKey(parentId);
        var cached = await GetCachedAsync<List<CategoryReadDto>>(cacheKey);
        if (cached is not null)
            return cached;

        var result = (await _repository.GetAllAsync())
            .Where(x => x.ParentId == parentId)
            .Select(Map)
            .ToList();

        await SetCachedAsync(cacheKey, result);
        return result;
    }

    private async Task InvalidateCategoryCachesAsync(int categoryId, int? parentId)
    {
        await _cache.RemoveAsync(AllCategoriesCacheKey);
        await _cache.RemoveAsync(GetCategoryCacheKey(categoryId));
        await _cache.RemoveAsync(GetRootCategoriesCacheKey());

        if (parentId.HasValue)
            await _cache.RemoveAsync(GetChildrenCacheKey(parentId.Value));
    }

    private async Task<T?> GetCachedAsync<T>(string key)
    {
        var json = await _cache.GetStringAsync(key);

        if (string.IsNullOrWhiteSpace(json))
            return default;

        return JsonSerializer.Deserialize<T>(json);
    }

    private Task SetCachedAsync<T>(string key, T value)
    {
        var json = JsonSerializer.Serialize(value);
        var minutes = _configuration.GetValue<int>("Redis:CategoryCacheMinutes", 10);

        return _cache.SetStringAsync(
            key,
            json,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes)
            });
    }

    private static CategoryReadDto Map(Category category) => new()
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
