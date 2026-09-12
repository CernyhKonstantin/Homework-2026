using HW_11._09._2026.DTOs.Product;
using HW_11._09._2026.Services.Interfaces;
using MediatR;

namespace HW_11._09._2026.CQRS.Commands.Products;

public sealed class CreateProductCommandHandler
    : IRequestHandler<CreateProductCommand, ProductReadDto>
{
    private readonly IProductService _productService;

    public CreateProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }

    public Task<ProductReadDto> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        return _productService.CreateAsync(request.ToDto());
    }
}
