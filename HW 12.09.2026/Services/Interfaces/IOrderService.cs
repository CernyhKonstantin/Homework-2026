using HW_12._09._2026.DTOs.Order;

namespace HW_12._09._2026.Services.Interfaces;

public interface IOrderService
{
    Task PublishOrderAsync(int userId, CreateOrderDto dto);
}
