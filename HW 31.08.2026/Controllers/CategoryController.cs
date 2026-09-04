using HW_31._08._2026.DTOs.Category;
using HW_31._08._2026.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HW_31._08._2026.Controllers;

[ApiController]
[Route("api/v1/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("root")]
    public async Task<IActionResult> GetRoot() =>
        Ok(await _service.GetRootCategoriesAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("{id:int}/children")]
    public async Task<IActionResult> GetChildren(int id) =>
        Ok(await _service.GetChildrenAsync(id));

    [Authorize(Roles = "Admin,Moderator")]
    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateDto dto) =>
        Ok(await _service.CreateAsync(dto));

    [Authorize(Roles = "Admin,Moderator")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CategoryCreateDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        return result is null ? NotFound() : Ok(result);
    }

    [Authorize(Roles = "Admin,Moderator")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        (await _service.DeleteAsync(id)) ? NoContent() : NotFound();
}
