using System.Text.Json;
using HW_24._08._2026.DTOs.Product;
using HW_24._08._2026.Models;
using HW_24._08._2026.Repositories.Interfaces;
using HW_24._08._2026.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace HW_24._08._2026.Services;

public class ProductService : IProductService
{
    private const string AllProductsCacheKey = "products:all";
    private static string GetProductCacheKey(int id) => $"products:id:{id}";
    private static string GetCategoryProductsCacheKey(int id) => $"products:category:{id}";

    private readonly IProductRepository _products;
    private readonly ICategoryRepository _categories;
    private readonly IConfiguration _configuration;
    private readonly IDistributedCache _cache;

    public ProductService(
        IProductRepository products,
        ICategoryRepository categories,
        IConfiguration configuration,
        IDistributedCache cache)
    {
        _products = products;
        _categories = categories;
        _configuration = configuration;
        _cache = cache;
    }

    public async Task<List<ProductReadDto>> GetAllAsync()
    {
        var cached = await GetCachedAsync<List<ProductReadDto>>(AllProductsCacheKey);
        if (cached is not null)
            return cached;

        var products = (await _products.GetAllAsync()).Select(Map).ToList();

        await SetCachedAsync(AllProductsCacheKey, products);

        foreach (var categoryId in products.Select(x => x.CategoryId).Distinct())
            await _cache.RemoveAsync(GetCategoryProductsCacheKey(categoryId));

        return products;
    }

    public async Task<ProductReadDto?> GetByIdAsync(int id)
    {
        var cacheKey = GetProductCacheKey(id);

        // 1. Always check Redis first.
        var cached = await GetCachedAsync<ProductReadDto>(cacheKey);
        if (cached is not null)
            return cached;

        // 2. Cache miss: load the product from the database.
        var product = await _products.GetByIdAsync(id);
        if (product is null)
            return null;

        var result = Map(product);

        // 3. Store the database result in Redis.
        await SetCachedAsync(cacheKey, result);

        return result;
    }

    public async Task<List<ProductReadDto>> GetByCategoryIdAsync(int categoryId)
    {
        var cacheKey = GetCategoryProductsCacheKey(categoryId);
        var cached = await GetCachedAsync<List<ProductReadDto>>(cacheKey);

        if (cached is not null)
            return cached;

        var products = (await _products.GetByCategoryIdAsync(categoryId))
            .Select(Map)
            .ToList();

        await SetCachedAsync(cacheKey, products);
        return products;
    }

    public async Task<ProductReadDto> CreateAsync(ProductCreateDto dto)
    {
        if (await _categories.GetByIdAsync(dto.CategoryId) is null)
            throw new ArgumentException("Category not found.");

        var maxImages = _configuration.GetValue<int>("ProductSettings:MaxImages", 5);
        if (dto.Images.Count > maxImages)
            throw new ArgumentException($"Maximum {maxImages} images allowed.");

        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            StockQty = dto.StockQty,
            IsActive = dto.IsActive,
            CategoryId = dto.CategoryId
        };

        await AddImagesAsync(product, dto.Images);

        var created = await _products.CreateAsync(product);
        var result = Map(created);

        await InvalidateProductCachesAsync(result.Id, result.CategoryId);
        await SetCachedAsync(GetProductCacheKey(result.Id), result);

        return result;
    }

    public async Task<ProductReadDto?> UpdateAsync(int id, ProductCreateDto dto)
    {
        var product = await _products.GetByIdAsync(id);
        if (product is null)
            return null;

        if (await _categories.GetByIdAsync(dto.CategoryId) is null)
            throw new ArgumentException("Category not found.");

        var maxImages = _configuration.GetValue<int>("ProductSettings:MaxImages", 5);
        if (dto.Images.Count > maxImages)
            throw new ArgumentException($"Maximum {maxImages} images allowed.");

        var oldCategoryId = product.CategoryId;

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.StockQty = dto.StockQty;
        product.IsActive = dto.IsActive;
        product.CategoryId = dto.CategoryId;
        product.UpdatedAt = DateTime.UtcNow;

        if (dto.Images.Count > 0)
        {
            product.Images.Clear();
            await AddImagesAsync(product, dto.Images);
        }

        await _products.UpdateAsync(product);

        var updated = await _products.GetByIdAsync(id) ?? product;
        var result = Map(updated);

        await _cache.RemoveAsync(AllProductsCacheKey);
        await _cache.RemoveAsync(GetProductCacheKey(id));
        await _cache.RemoveAsync(GetCategoryProductsCacheKey(oldCategoryId));
        await _cache.RemoveAsync(GetCategoryProductsCacheKey(result.CategoryId));

        await SetCachedAsync(GetProductCacheKey(id), result);

        return result;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _products.GetByIdAsync(id);
        if (product is null)
            return false;

        await _products.DeleteAsync(product);
        await InvalidateProductCachesAsync(id, product.CategoryId);

        return true;
    }

    private async Task InvalidateProductCachesAsync(int productId, int categoryId)
    {
        await _cache.RemoveAsync(AllProductsCacheKey);
        await _cache.RemoveAsync(GetProductCacheKey(productId));
        await _cache.RemoveAsync(GetCategoryProductsCacheKey(categoryId));
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
        var minutes = _configuration.GetValue<int>("Redis:ProductCacheMinutes", 10);

        return _cache.SetStringAsync(
            key,
            json,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes)
            });
    }

    private async Task AddImagesAsync(
        Product product,
        IEnumerable<Microsoft.AspNetCore.Http.IFormFile> files)
    {
        var folder = Path.Combine(
            Directory.GetCurrentDirectory(),
            _configuration["ProductSettings:UploadFolder"]
                ?? "wwwroot/uploads/products");

        Directory.CreateDirectory(folder);

        var allowed = _configuration
            .GetSection("ProductSettings:AllowedExtensions")
            .Get<string[]>()
            ?? [".jpg", ".jpeg", ".png", ".webp"];

        foreach (var file in files)
        {
            if (file.Length <= 0)
                continue;

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowed.Contains(extension, StringComparer.OrdinalIgnoreCase))
                throw new ArgumentException("Unsupported image format.");

            var fileName = $"{Guid.NewGuid():N}{extension}";
            var filePath = Path.Combine(folder, fileName);

            await using var stream = File.Create(filePath);
            await file.CopyToAsync(stream);

            product.Images.Add(new ProductImage
            {
                ImageUrl = $"/uploads/products/{fileName}"
            });
        }
    }

    private static ProductReadDto Map(Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Description = product.Description,
        Price = product.Price,
        StockQty = product.StockQty,
        IsActive = product.IsActive,
        CategoryId = product.CategoryId,
        CategoryName = product.Category?.Name ?? string.Empty,
        Images = product.Images
            .Select(image => new ProductImageReadDto
            {
                Id = image.Id,
                ImageUrl = image.ImageUrl
            })
            .ToList()
    };
}
