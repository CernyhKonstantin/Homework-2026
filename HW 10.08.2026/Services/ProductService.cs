using HW_10._08._2026.DTOs.Product;
using HW_10._08._2026.Models;
using HW_10._08._2026.Repositories.Interfaces;
using HW_10._08._2026.Services.Interfaces;

namespace HW_10._08._2026.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _products;
    private readonly ICategoryRepository _categories;
    private readonly IConfiguration _configuration;

    public ProductService(
        IProductRepository products,
        ICategoryRepository categories,
        IConfiguration configuration)
    {
        _products = products;
        _categories = categories;
        _configuration = configuration;
    }

    public async Task<List<ProductReadDto>> GetAllAsync() =>
        (await _products.GetAllAsync()).Select(Map).ToList();

    public async Task<ProductReadDto?> GetByIdAsync(int id)
    {
        var product = await _products.GetByIdAsync(id);
        return product == null ? null : Map(product);
    }

    public async Task<List<ProductReadDto>> GetByCategoryIdAsync(int categoryId) =>
        (await _products.GetByCategoryIdAsync(categoryId)).Select(Map).ToList();

    public async Task<ProductReadDto> CreateAsync(ProductCreateDto dto)
    {
        if (await _categories.GetByIdAsync(dto.CategoryId) == null)
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
        return Map(await _products.CreateAsync(product));
    }

    public async Task<ProductReadDto?> UpdateAsync(int id, ProductCreateDto dto)
    {
        var product = await _products.GetByIdAsync(id);
        if (product == null) return null;

        if (await _categories.GetByIdAsync(dto.CategoryId) == null)
            throw new ArgumentException("Category not found.");

        var maxImages = _configuration.GetValue<int>("ProductSettings:MaxImages", 5);
        if (dto.Images.Count > maxImages)
            throw new ArgumentException($"Maximum {maxImages} images allowed.");

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
        return Map(await _products.GetByIdAsync(id) ?? product);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _products.GetByIdAsync(id);
        if (product == null) return false;

        await _products.DeleteAsync(product);
        return true;
    }

    private async Task AddImagesAsync(Product product, IEnumerable<IFormFile> files)
    {
        var folder = Path.Combine(
            Directory.GetCurrentDirectory(),
            _configuration["ProductSettings:UploadFolder"] ?? "wwwroot/uploads/products");

        Directory.CreateDirectory(folder);

        var allowed = _configuration
            .GetSection("ProductSettings:AllowedExtensions")
            .Get<string[]>() ?? [".jpg", ".jpeg", ".png", ".webp"];

        foreach (var file in files)
        {
            if (file.Length <= 0) continue;

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowed.Contains(extension, StringComparer.OrdinalIgnoreCase))
                throw new ArgumentException("Unsupported image format.");

            var name = $"{Guid.NewGuid():N}{extension}";
            var path = Path.Combine(folder, name);

            await using var stream = File.Create(path);
            await file.CopyToAsync(stream);

            product.Images.Add(new ProductImage
            {
                ImageUrl = $"/uploads/products/{name}"
            });
        }
    }

    private static ProductReadDto Map(Product p) => new()
    {
        Id = p.Id,
        Name = p.Name,
        Description = p.Description,
        Price = p.Price,
        StockQty = p.StockQty,
        IsActive = p.IsActive,
        CategoryId = p.CategoryId,
        CategoryName = p.Category?.Name ?? string.Empty,
        Images = p.Images.Select(i => new ProductImageReadDto
        {
            Id = i.Id,
            ImageUrl = i.ImageUrl
        }).ToList()
    };
}
