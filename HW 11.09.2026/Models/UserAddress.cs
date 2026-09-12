using System.ComponentModel.DataAnnotations;

namespace HW_11._09._2026.Models;

public class UserAddress
{
    public int Id { get; set; }

    public int UserId { get; set; }

    [Required, MaxLength(100)]
    public string Label { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string RecipientName { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Country { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string City { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    public string PostalCode { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string Street { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    public string HouseNumber { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Apartment { get; set; } = string.Empty;

    [MaxLength(30)]
    public string Phone { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
}
