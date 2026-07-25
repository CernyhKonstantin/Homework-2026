using HW_06._07._2026.DTOs.Product;
using HW_06._07._2026.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HW_06._07._2026.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<ActionResult<ProductReadDto>> Create([FromBody] ProductCreateDto dto)
    {
        try
        {
            var createdProduct = await _productService.CreateAsync(dto);
            return Ok(createdProduct);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductReadDto>>> GetAll()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductReadDto>> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound(new { message = "Product not found." });
        }

        return Ok(product);
    }
}