using HW_08._07._2026.DTOs.Category;
using HW_08._07._2026.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HW_08._07._2026.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<List<CategoryReadDto>>> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryReadDto>> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
                return NotFound($"Category with id {id} was not found.");

            return Ok(category);
        }

        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<CategoryReadDto>> Create(CategoryCreateDto dto)
        {
            var createdCategory = await _categoryService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdCategory.Id },
                createdCategory
            );
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryUpdateDto dto)
        {
            var updated = await _categoryService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound($"Category with id {id} was not found.");

            return NoContent();
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _categoryService.DeleteAsync(id);

            if (!deleted)
                return NotFound($"Category with id {id} was not found.");

            return NoContent();
        }

        // 1) GET: api/Category/5/parents
        [HttpGet("{id}/parents")]
        public async Task<ActionResult<List<CategoryReadDto>>> GetParents(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
                return NotFound($"Category with id {id} was not found.");

            var parents = await _categoryService.GetParentsByCategoryIdAsync(id);
            return Ok(parents);
        }

        // 2) GET: api/Category/5/children
        [HttpGet("{id}/children")]
        public async Task<ActionResult<List<CategoryReadDto>>> GetChildren(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
                return NotFound($"Category with id {id} was not found.");

            var children = await _categoryService.GetChildrenByCategoryIdAsync(id);
            return Ok(children);
        }

        // 3) GET: api/Category/tree
        [HttpGet("tree")]
        public async Task<ActionResult<List<CategoryTreeDto>>> GetTree()
        {
            var tree = await _categoryService.GetCategoryTreeAsync();
            return Ok(tree);
        }
    }
}