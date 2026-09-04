using System.ComponentModel.DataAnnotations;

namespace HW_05._09._2026.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [Required, MaxLength(30)]
    public string Status { get; set; } = "Pending";

    public bool Paid { get; set; }
    public ICollection<OrderDetail> Details { get; set; } = new List<OrderDetail>();
}
