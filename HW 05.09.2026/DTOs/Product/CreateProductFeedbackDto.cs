using System.ComponentModel.DataAnnotations;

namespace HW_05._09._2026.DTOs.Product;

public class CreateProductFeedbackDto
{
    [Required]
    [RegularExpression("^(review|question)$", ErrorMessage = "Type must be either review or question.")]
    public string Type { get; set; } = string.Empty;

    [Required]
    [StringLength(2000, MinimumLength = 2)]
    public string Message { get; set; } = string.Empty;

    [Range(1, 5)]
    public int? Rating { get; set; }
}
