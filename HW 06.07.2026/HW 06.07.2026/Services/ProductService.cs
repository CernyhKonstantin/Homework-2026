using HW_06._07._2026.DTOs.Product;
using HW_06._07._2026.Models;
using HW_06._07._2026.Repositories.Interfaces;
using HW_06._07._2026.Services.Interfaces;

namespace HW_06._07._2026.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IConfiguration _configuration;

    public ProductService(IProductRepository productRepository, IConfiguration configuration)
    {
        _productRepository = productRepository;
        _configuration = configuration;
    }

    public async Task<ProductReadDto> CreateAsync(ProductCreateDto dto)
    {
        int maxImages = _configuration.GetValue<int>("ProductSettings:MaxImages");

        if (dto.Images.Count > maxImages)
        {
            throw new Exception($"You can upload maximum {maxImages} images.");
        }

        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            StockQty = dto.StockQty,
            IsActive = dto.IsActive,
            CategoryId = dto.CategoryId,
            Images = dto.Images.Select(image => new ProductImage
            {
                Url = image.Url,
                IsPrimary = image.IsPrimary
            }).ToList()
        };

        var createdProduct = await _productRepository.CreateAsync(product);

        return MapToReadDto(createdProduct);
    }

    public async Task<List<ProductReadDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products.Select(MapToReadDto).ToList();
    }

    public async Task<ProductReadDto?> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return null;
        }

        return MapToReadDto(product);
    }

    private static ProductReadDto MapToReadDto(Product product)
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
            Images = product.Images.Select(image => new ProductImageReadDto
            {
                Id = image.Id,
                Url = image.Url,
                IsPrimary = image.IsPrimary
            }).ToList()
        };
    }
}