using HW_10._07._2026.DTOs.Product;
using HW_10._07._2026.Repositories.Interfaces;
using HW_10._07._2026.Services.Interfaces;

namespace HW_10._07._2026.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ProductReadDto>> GetByCategoryIdAsync(int categoryId)
    {
        var products = await _repository.GetByCategoryIdAsync(categoryId);

        return products.Select(product => new ProductReadDto
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

        }).ToList();
    }
}