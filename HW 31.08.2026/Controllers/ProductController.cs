using HW_31._08._2026.DTOs.Product;
using HW_31._08._2026.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HW_31._08._2026.Controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("category/{categoryId:int}")]
    public async Task<IActionResult> GetByCategory(int categoryId) =>
        Ok(await _service.GetByCategoryIdAsync(categoryId));

    [Authorize(Roles = "Admin,Moderator")]
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] ProductCreateDto dto)
    {
        try
        {
            return Ok(await _service.CreateAsync(dto));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "Admin,Moderator")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromForm] ProductCreateDto dto)
    {
        try
        {
            var result = await _service.UpdateAsync(id, dto);
            return result is null ? NotFound() : Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "Admin,Moderator")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        (await _service.DeleteAsync(id)) ? NoContent() : NotFound();
}
