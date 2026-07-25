namespace HW_24_06_2026.DTOs;

/// <summary>DTO used for updating a product.</summary>
public class UpdateProductDto
{
    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }
}