using HW_01_07_2026.DTOs.Category;
using HW_01_07_2026.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HW_01_07_2026.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryReadDto>>> GetAllCategories()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryReadDto>> GetCategoryById(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        if (category == null)
            return NotFound($"Category with id {id} not found.");

        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryReadDto>> CreateCategory(CategoryCreateDto dto)
    {
        var createdCategory = await _categoryService.CreateAsync(dto);
        return Ok(createdCategory);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryReadDto>> UpdateCategory(int id, CategoryUpdateDto dto)
    {
        var updatedCategory = await _categoryService.UpdateAsync(id, dto);

        if (updatedCategory == null)
            return NotFound($"Category with id {id} not found.");

        return Ok(updatedCategory);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        var deleted = await _categoryService.DeleteAsync(id);

        if (!deleted)
            return NotFound($"Category with id {id} not found.");

        return Ok(new { message = $"Category with id {id} deleted successfully." });
    }
}