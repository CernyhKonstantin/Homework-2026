using HW_31._08._2026.DTOs.Order;

namespace HW_31._08._2026.Services.Interfaces;

public interface IOrderService
{
    Task PublishOrderAsync(int userId, CreateOrderDto dto);
}
