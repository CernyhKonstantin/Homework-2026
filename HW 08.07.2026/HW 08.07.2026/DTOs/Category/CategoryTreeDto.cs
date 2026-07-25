namespace HW_08._07._2026.DTOs.Category
{
    public class CategoryTreeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public int? ParentId { get; set; }

        public List<CategoryTreeDto> Children { get; set; } = new();
    }
}