using HW_14._08._2026.DTOs.Category;

namespace HW_14._08._2026.DTOs.Product;

public class ProductReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQty { get; set; }
    public bool IsActive { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public CategoryReadDto? Category { get; set; }
    public List<ProductImageReadDto> Images { get; set; } = new();
}
