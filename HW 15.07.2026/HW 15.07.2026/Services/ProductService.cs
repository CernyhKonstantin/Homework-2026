using HW_15._07._2026.DTOs.Category;
using HW_15._07._2026.DTOs.Product;
using HW_15._07._2026.Models;
using HW_15._07._2026.Repositories.Interfaces;
using HW_15._07._2026.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace HW_15._07._2026.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IConfiguration _configuration;

    public ProductService(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IConfiguration configuration)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _configuration = configuration;
    }

    private ProductReadDto MapToDto(Product product)
    {
        return new ProductReadDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQty = product.StockQty,
            IsActive = product.IsActive,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name ?? string.Empty,

            Category = product.Category == null
                ? null
                : new CategoryReadDto
                {
                    Id = product.Category.Id,
                    Name = product.Category.Name,
                    Slug = product.Category.Slug,
                    ParentId = product.Category.ParentId,
                    ParentName = product.Category.Parent?.Name,
                    CreatedAt = product.Category.CreatedAt,
                    UpdatedAt = product.Category.UpdatedAt
                },

            Images = product.Images
                .Select(x => new ProductImageReadDto
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl
                })
                .ToList()
        };
    }

    private List<ProductReadDto> MapToDtoList(IEnumerable<Product> products)
    {
        return products
            .Select(MapToDto)
            .ToList();
    }

    public async Task<List<ProductReadDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();

        return MapToDtoList(products);
    }

    public async Task<ProductReadDto?> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return null;
        }

        return MapToDto(product);
    }

    public async Task<List<ProductReadDto>> GetByCategoryIdAsync(int categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);

        if (category == null)
        {
            return new List<ProductReadDto>();
        }

        var products = await _productRepository.GetByCategoryIdAsync(categoryId);

        return MapToDtoList(products);
    }

    public async Task<ProductReadDto> CreateAsync(ProductCreateDto dto)
    {
        var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);

        if (category == null)
        {
            throw new Exception("Category not found.");
        }

        int maxImages = _configuration.GetValue<int>("ProductSettings:MaxImages");

        if (dto.Images.Count > maxImages)
        {
            throw new Exception($"Maximum {maxImages} images allowed.");
        }

        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            StockQty = dto.StockQty,
            IsActive = dto.IsActive,
            CategoryId = dto.CategoryId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        foreach (var file in dto.Images)
        {
            if (file.Length == 0)
            {
                continue;
            }

            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "products");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName =
                Guid.NewGuid().ToString() +
                Path.GetExtension(file.FileName);

            var filePath = Path.Combine(
                uploadsFolder,
                fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            product.Images.Add(new ProductImage
            {
                ImageUrl = "/uploads/products/" + fileName
            });
        }

        product = await _productRepository.CreateAsync(product);

        return MapToDto(product);
    }

    public async Task<ProductReadDto?> UpdateAsync(
    int id,
    ProductCreateDto dto)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return null;
        }

        var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);

        if (category == null)
        {
            throw new Exception("Category not found.");
        }

        int maxImages = _configuration.GetValue<int>("ProductSettings:MaxImages");

        if (dto.Images.Count > maxImages)
        {
            throw new Exception($"Maximum {maxImages} images allowed.");
        }

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

            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "products");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            foreach (var file in dto.Images)
            {
                if (file.Length == 0)
                {
                    continue;
                }

                var fileName =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(file.FileName);

                var filePath = Path.Combine(
                    uploadsFolder,
                    fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                product.Images.Add(new ProductImage
                {
                    ImageUrl = "/uploads/products/" + fileName
                });
            }
        }

        await _productRepository.UpdateAsync(product);

        product = await _productRepository.GetByIdAsync(id);

        return product == null ? null : MapToDto(product);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return false;
        }

        foreach (var image in product.Images)
        {
            if (string.IsNullOrWhiteSpace(image.ImageUrl))
            {
                continue;
            }

            var relativePath = image.ImageUrl.TrimStart('/')
                                              .Replace('/', Path.DirectorySeparatorChar);

            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                relativePath);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        await _productRepository.DeleteAsync(product);

        return true;
    }
}