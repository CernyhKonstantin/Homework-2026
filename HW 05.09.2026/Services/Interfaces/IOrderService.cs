using HW_05._09._2026.DTOs.Order;

namespace HW_05._09._2026.Services.Interfaces;

public interface IOrderService
{
    Task PublishOrderAsync(int userId, CreateOrderDto dto);
}
