using Microsoft.AspNetCore.Mvc;
using HW_22_06_2026.DTOs;
using HW_22_06_2026.Services;

namespace HW_22_06_2026.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var product = _service.GetById(id);

        if (product == null)
            return NotFound("Product not found");

        return Ok(product);
    }

    [HttpPost]
    public IActionResult Create(CreateProductDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Name is required");

        var product = _service.Create(dto);

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, UpdateProductDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data");

        var product = _service.Update(id, dto);

        if (product == null)
            return NotFound("Product not found");

        return Ok(product);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = _service.Delete(id);

        if (!result)
            return NotFound("Product not found");

        return NoContent();
    }

    [HttpGet("search")]
    public IActionResult Search(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Name parameter is required");

        return Ok(_service.Search(name));
    }
}