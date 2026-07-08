namespace HW_06._07._2026.DTOs.Product;

public class ProductCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQty { get; set; }
    public bool IsActive { get; set; } = true;
    public int CategoryId { get; set; }

    public List<ProductImageCreateDto> Images { get; set; } = new();
}