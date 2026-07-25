using System.ComponentModel.DataAnnotations;

namespace HW_22_06_2026.DTOs;

public class UpdateProductDto
{
    [Required]
    public string Name { get; set; } = "";

    public decimal Price { get; set; }
}