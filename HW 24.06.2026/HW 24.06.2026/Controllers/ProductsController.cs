using HW_24_06_2026.DTOs;
using HW_24_06_2026.Models;
using HW_24_06_2026.Services;
using Microsoft.AspNetCore.Mvc;

namespace HW_24_06_2026.Controllers;

/// <summary>Controller for managing products.</summary>
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    /// <summary>Get all products.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<Product>), 200)]
    public IActionResult GetAll() => Ok(_service.GetAll());

    /// <summary>Get product by id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Product), 200)]
    [ProducesResponseType(404)]
    public IActionResult GetById(int id)
    {
        var product = _service.GetById(id);
        if (product == null) return NotFound("Product not found");
        return Ok(product);
    }

    /// <summary>Create a new product.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(Product), 201)]
    [ProducesResponseType(400)]
    public IActionResult Create(CreateProductDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return BadRequest("Name is required");

        var product = _service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    /// <summary>Update product.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Product), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult Update(int id, UpdateProductDto dto)
    {
        var product = _service.Update(id, dto);
        if (product == null) return NotFound("Product not found");
        return Ok(product);
    }

    /// <summary>Delete product.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult Delete(int id)
    {
        var result = _service.Delete(id);
        if (!result) return NotFound("Product not found");
        return NoContent();
    }

    /// <summary>Search products by name.</summary>
    [HttpGet("search")]
    [ProducesResponseType(typeof(List<Product>), 200)]
    [ProducesResponseType(400)]
    public IActionResult Search(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Name parameter is required");

        return Ok(_service.Search(name));
    }
}