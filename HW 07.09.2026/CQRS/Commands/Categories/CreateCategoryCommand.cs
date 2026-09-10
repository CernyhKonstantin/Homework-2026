using HW_07._09._2026.DTOs.Category;
using MediatR;

namespace HW_07._09._2026.CQRS.Commands.Categories;

public sealed record CreateCategoryCommand(
    string Name,
    string Slug,
    int? ParentId) : IRequest<CategoryReadDto>;
