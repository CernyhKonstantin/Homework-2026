namespace HW_06._07._2026.DTOs.Product;

public class ProductReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQty { get; set; }
    public bool IsActive { get; set; }
    public int CategoryId { get; set; }

    public List<ProductImageReadDto> Images { get; set; } = new();
}