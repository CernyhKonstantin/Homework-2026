using System.ComponentModel.DataAnnotations;

namespace HW_05._09._2026.DTOs.Order;

public class CreateOrderDto
{
    public List<CreateOrderItemDto> Products { get; set; } = new();
    public bool Paid { get; set; }
}

public class CreateOrderItemDto
{
    [Range(1, int.MaxValue)]
    public int ProductId { get; set; }

    [Range(1, int.MaxValue)]
    public int Count { get; set; }
}
