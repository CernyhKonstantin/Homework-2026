using HW_14._09._2026.DTOs.Category;

namespace HW_14._09._2026.ViewModels;

public sealed class CategoriesPageViewModel
{
    public IReadOnlyList<CategoryReadDto> Categories { get; init; } = [];
}
