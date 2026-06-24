namespace HW_24_06_2026.DTOs;

/// <summary>DTO used for creating a product.</summary>
public class CreateProductDto
{
    /// <summary>Product name (required).</summary>
    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }
}