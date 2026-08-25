using System.Security.Claims;
using HW_24._08._2026.DTOs.Order;
using HW_24._08._2026.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HW_24._08._2026.Controllers;

[ApiController]
[Route("api/v1/orders")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService) => _orderService = orderService;

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdValue, out var userId))
            return Unauthorized();

        try
        {
            await _orderService.PublishOrderAsync(userId, dto);
            return Accepted(new { message = "Order accepted and placed into the Orders queue." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
