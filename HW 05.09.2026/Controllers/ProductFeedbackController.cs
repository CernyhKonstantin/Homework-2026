using HW_05._09._2026.DTOs.Product;
using HW_05._09._2026.Repositories.Interfaces;
using HW_05._09._2026.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HW_05._09._2026.Controllers;

[ApiController]
[Route("api/v1/products/{productId:int}/feedback")]
public class ProductFeedbackController : ControllerBase
{
    private readonly IProductFeedbackService _feedbackService;
    private readonly IProductRepository _productRepository;

    public ProductFeedbackController(
        IProductFeedbackService feedbackService,
        IProductRepository productRepository)
    {
        _feedbackService = feedbackService;
        _productRepository = productRepository;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(int productId, [FromBody] CreateProductFeedbackDto dto)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null)
            return NotFound(new { message = "Product not found." });

        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = User.FindFirstValue(ClaimTypes.Email);

        if (!int.TryParse(userIdValue, out var userId) || string.IsNullOrWhiteSpace(email))
            return Unauthorized(new { message = "The authenticated user information is invalid." });

        if (dto.Type.Equals("question", StringComparison.OrdinalIgnoreCase) && dto.Rating.HasValue)
            return BadRequest(new { message = "A question cannot contain a rating." });

        if (dto.Type.Equals("review", StringComparison.OrdinalIgnoreCase) && !dto.Rating.HasValue)
            return BadRequest(new { message = "A review must contain a rating from 1 to 5." });

        var result = await _feedbackService.CreateAsync(
            productId,
            userId,
            email,
            dto.Type,
            dto.Message,
            dto.Rating);

        return CreatedAtAction(nameof(GetByProduct), new { productId }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetByProduct(int productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null)
            return NotFound(new { message = "Product not found." });

        return Ok(await _feedbackService.GetByProductIdAsync(productId));
    }
}
