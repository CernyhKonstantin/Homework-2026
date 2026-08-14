namespace HW_14._08._2026.DTOs.Category;

public class CategoryCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int? ParentId { get; set; }
}
