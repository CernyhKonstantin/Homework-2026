using System.ComponentModel.DataAnnotations;

namespace HW_12._09._2026.DTOs.Auth;

public class ExternalAuthDto
{
    [Required, EmailAddress, MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string Provider { get; set; } = string.Empty;

    [Required, MaxLength(255)]
    public string NumberProvider { get; set; } = string.Empty;
}
