using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HW_29_06_2026.Data;
using HW_29_06_2026.DTOs.Category;
using HW_29_06_2026.Models;

namespace HW_29_06_2026.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ShopDbContext _context;

    public CategoryController(ShopDbContext context)
    {
        _context = context;
    }

    // 1) Create Category
    [HttpPost]
    public async Task<ActionResult<CategoryReadDto>> CreateCategory(CategoryCreateDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            Slug = dto.Slug,
            ParentId = dto.ParentId
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        var result = new CategoryReadDto
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            ParentId = category.ParentId
        };

        return Ok(result);
    }

    // 2) Get all categories
    [HttpGet]
    public async Task<ActionResult<List<CategoryReadDto>>> GetAllCategories()
    {
        var categories = await _context.Categories
            .Select(c => new CategoryReadDto
            {
                Id = c.Id,
                Name = c.Name,
                Slug = c.Slug,
                ParentId = c.ParentId
            })
            .ToListAsync();

        return Ok(categories);
    }

    // 3) Get category by id
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryReadDto>> GetCategoryById(int id)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
            return NotFound($"Category with id {id} not found.");

        var result = new CategoryReadDto
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            ParentId = category.ParentId
        };

        return Ok(result);
    }
}