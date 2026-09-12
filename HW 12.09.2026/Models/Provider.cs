using System.ComponentModel.DataAnnotations;

namespace HW_12._09._2026.Models;

public class Provider
{
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public ICollection<UserProvider> UserProviders { get; set; } = new List<UserProvider>();
}
