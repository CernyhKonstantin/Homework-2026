using HW_09._09._2026.DTOs.Category;
using HW_09._09._2026.Services.Interfaces;
using MediatR;

namespace HW_09._09._2026.CQRS.Commands.Categories;

public sealed class CreateCategoryCommandHandler
    : IRequestHandler<CreateCategoryCommand, CategoryReadDto>
{
    private readonly ICategoryService _categoryService;

    public CreateCategoryCommandHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public Task<CategoryReadDto> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var dto = new CategoryCreateDto
        {
            Name = request.Name,
            Slug = request.Slug,
            ParentId = request.ParentId
        };

        return _categoryService.CreateAsync(dto);
    }
}
