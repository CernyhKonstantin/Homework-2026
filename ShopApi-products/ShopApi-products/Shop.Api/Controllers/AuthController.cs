using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Api.Models;
using Shop.Api.Requests.Categories;

namespace Shop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly AppDbContext _context; 

    public CategoryController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] CategoryQueryParameters queryParameters)
    {
        if (queryParameters.PageNumber < 1)
        {
            queryParameters.PageNumber = 1;
        }

        if (queryParameters.PageSize < 1)
        {
            queryParameters.PageSize = 10;
        }

        var totalCount = await _context.Categories.CountAsync();

        var categories = await _context.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name) 
            .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
            .Take(queryParameters.PageSize)
            .ToListAsync();

        var result = new PagedResult<Category>(
            categories,
            totalCount,
            queryParameters.PageNumber,
            queryParameters.PageSize
        );

        return Ok(result);
    }
}