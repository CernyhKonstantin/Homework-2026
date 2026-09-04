using System.ComponentModel.DataAnnotations;

namespace HW_31._08._2026.Models;

public class ProductImage
{
    public int Id { get; set; }

    [Required, MaxLength(500)]
    public string ImageUrl { get; set; } = string.Empty;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}
