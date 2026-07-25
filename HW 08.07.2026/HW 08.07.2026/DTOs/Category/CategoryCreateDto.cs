using System.ComponentModel.DataAnnotations;

namespace HW_08._07._2026.DTOs.Category
{
    public class CategoryCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Slug { get; set; } = string.Empty;

        public int? ParentId { get; set; }
    }
}