using HW_09._09._2026.DTOs.Category;
using HW_09._09._2026.Services.Interfaces;
using MediatR;

namespace HW_09._09._2026.CQRS.Queries.Categories;

public sealed class GetCategoryBySlugQueryHandler
    : IRequestHandler<GetCategoryBySlugQuery, CategoryReadDto?>
{
    private readonly ICategoryService _categoryService;

    public GetCategoryBySlugQueryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public Task<CategoryReadDto?> Handle(
        GetCategoryBySlugQuery request,
        CancellationToken cancellationToken)
    {
        return _categoryService.GetBySlugAsync(request.Slug);
    }
}
