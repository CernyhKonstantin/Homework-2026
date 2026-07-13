using HW_10._07._2026.DTOs.Product;
using HW_10._07._2026.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HW_10._07._2026.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    // GET: api/product/category/1
    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<List<ProductReadDto>>> GetProductsByCategory(int categoryId)
    {
        var result = await _service.GetByCategoryIdAsync(categoryId);
        return Ok(result);
    }
}