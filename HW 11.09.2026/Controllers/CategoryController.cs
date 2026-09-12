using HW_11._09._2026.CQRS.Commands.Categories;
using HW_11._09._2026.CQRS.Queries.Categories;
using HW_11._09._2026.DTOs.Category;
using HW_11._09._2026.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HW_11._09._2026.Controllers;

[ApiController]
[Route("api/v1/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;
    private readonly IMediator _mediator;

    public CategoryController(ICategoryService service, IMediator mediator)
    {
        _service = service;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("root")]
    public async Task<IActionResult> GetRoot() =>
        Ok(await _service.GetRootCategoriesAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryReadDto>> GetCategoryById(int id)
    {
        var dto = await _mediator.Send(new GetCategoryByIdQuery(id));

        if (dto is null)
            return NotFound();

        return Ok(dto);
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<CategoryReadDto>> GetCategoryBySlug(string slug)
    {
        var dto = await _mediator.Send(new GetCategoryBySlugQuery(slug));

        if (dto is null)
            return NotFound();

        return Ok(dto);
    }

    [HttpGet("{id:int}/children")]
    public async Task<IActionResult> GetChildren(int id) =>
        Ok(await _service.GetChildrenAsync(id));

    [Authorize(Roles = "Admin,Moderator")]
    [HttpPost]
    public async Task<ActionResult<CategoryReadDto>> Create(CategoryCreateDto dto)
    {
        try
        {
            var result = await _mediator.Send(new CreateCategoryCommand(
                dto.Name,
                dto.Slug,
                dto.ParentId));

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

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
