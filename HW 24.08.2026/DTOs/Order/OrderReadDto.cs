namespace HW_24._08._2026.DTOs.Order;

public class OrderReadDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool Paid { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderDetailReadDto> Details { get; set; } = new();
}

public class OrderDetailReadDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Count { get; set; }
    public decimal Total { get; set; }
}
