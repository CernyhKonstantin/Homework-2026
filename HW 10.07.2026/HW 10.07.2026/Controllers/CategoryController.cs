using HW_10._07._2026.DTOs.Category;
using HW_10._07._2026.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HW_10._07._2026.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    // GET: api/category/root
    [HttpGet("root")]
    public async Task<ActionResult<List<CategoryReadDto>>> GetRootCategories()
    {
        var result = await _service.GetRootCategoriesAsync();
        return Ok(result);
    }

    // GET: api/category/5/children
    [HttpGet("{id}/children")]
    public async Task<ActionResult<List<CategoryReadDto>>> GetChildren(int id)
    {
        var result = await _service.GetChildrenAsync(id);
        return Ok(result);
    }

    // GET: api/category/tree
    [HttpGet("tree")]
    public async Task<ActionResult<List<CategoryTreeDto>>> GetTree()
    {
        var result = await _service.GetTreeAsync();
        return Ok(result);
    }

    // GET: api/category/search?name=phone
    [HttpGet("search")]
    public async Task<ActionResult<List<CategorySearchDto>>> Search([FromQuery] string name)
    {
        var result = await _service.SearchAsync(name);
        return Ok(result);
    }
}