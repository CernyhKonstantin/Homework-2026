using HW_07._09._2026.DTOs.Category;
using MediatR;

namespace HW_07._09._2026.CQRS.Queries.Categories;

public sealed record GetCategoryByIdQuery(int Id) : IRequest<CategoryReadDto?>;
