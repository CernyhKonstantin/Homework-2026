using HW_09._09._2026.DTOs.Category;
using HW_09._09._2026.Services.Interfaces;
using MediatR;

namespace HW_09._09._2026.CQRS.Queries.Categories;

public sealed class GetCategoryByIdQueryHandler
    : IRequestHandler<GetCategoryByIdQuery, CategoryReadDto?>
{
    private readonly ICategoryService _categoryService;

    public GetCategoryByIdQueryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public Task<CategoryReadDto?> Handle(
        GetCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        return _categoryService.GetByIdAsync(request.Id);
    }
}
