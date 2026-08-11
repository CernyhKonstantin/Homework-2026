using Microsoft.AspNetCore.Http;

namespace HW_10._08._2026.DTOs.Product;

public class ProductCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQty { get; set; }
    public bool IsActive { get; set; } = true;
    public int CategoryId { get; set; }
    public List<IFormFile> Images { get; set; } = new();
}
