using HW_06._07._2026.DTOs.Category;
using HW_06._07._2026.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HW_06._07._2026.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<ActionResult<CategoryReadDto>> Create([FromBody] CategoryCreateDto dto)
    {
        var createdCategory = await _categoryService.CreateAsync(dto);
        return Ok(createdCategory);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryReadDto>> GetById(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }
}