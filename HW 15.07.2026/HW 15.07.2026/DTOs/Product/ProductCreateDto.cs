using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace HW_15._07._2026.DTOs.Product;

public class ProductCreateDto
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Range(0.01, 1000000)]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int StockQty { get; set; }

    public bool IsActive { get; set; } = true;

    [Required]
    public int CategoryId { get; set; }

    public List<IFormFile> Images { get; set; } = new();
}