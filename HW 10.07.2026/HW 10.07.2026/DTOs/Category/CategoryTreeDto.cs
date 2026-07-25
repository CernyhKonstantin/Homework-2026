namespace HW_10._07._2026.DTOs.Category;

public class CategoryTreeDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<CategoryTreeDto> Children { get; set; } = new();
}