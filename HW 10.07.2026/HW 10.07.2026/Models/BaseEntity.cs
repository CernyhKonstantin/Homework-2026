using System.ComponentModel.DataAnnotations.Schema;

namespace HW_10._07._2026.Models;

public abstract class BaseEntity
{
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}