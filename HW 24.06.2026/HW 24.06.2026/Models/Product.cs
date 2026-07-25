namespace HW_24_06_2026.Models;

/// <summary>
/// Represents a product in the system.
/// </summary>
public class Product
{
    /// <summary>Unique identifier of the product.</summary>
    public int Id { get; set; }

    /// <summary>Name of the product.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Price of the product.</summary>
    public decimal Price { get; set; }
}