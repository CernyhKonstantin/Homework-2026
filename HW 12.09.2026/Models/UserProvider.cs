using System.ComponentModel.DataAnnotations;

namespace HW_12._09._2026.Models;

public class UserProvider
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public int ProviderId { get; set; }

    [Required, MaxLength(255)]
    public string NumberProvider { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
    public Provider Provider { get; set; } = null!;
}
