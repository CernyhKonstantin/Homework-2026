using HW_11._09._2026.DTOs.Product;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HW_11._09._2026.CQRS.Commands.Products;

public sealed class CreateProductCommand : IRequest<ProductReadDto>
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int StockQty { get; init; }
    public bool IsActive { get; init; } = true;
    public int CategoryId { get; init; }
    public List<IFormFile> Images { get; init; } = new();

    public ProductCreateDto ToDto() => new()
    {
        Name = Name,
        Description = Description,
        Price = Price,
        StockQty = StockQty,
        IsActive = IsActive,
        CategoryId = CategoryId,
        Images = Images
    };
}
