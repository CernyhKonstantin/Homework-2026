namespace HW_06._07._2026.DTOs.Product;

public class ProductImageReadDto
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }
}