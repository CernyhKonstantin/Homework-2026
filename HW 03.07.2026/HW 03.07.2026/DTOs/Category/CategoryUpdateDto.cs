namespace HW_03_07_2026.DTOs.Category;

public class CategoryUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int? ParentId { get; set; }
}