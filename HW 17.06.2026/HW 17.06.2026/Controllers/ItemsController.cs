using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private static List<Item> items = new List<Item>
        {
            new Item { Id = 1, Name = "Pen" },
            new Item { Id = 2, Name = "Notebook" },
            new Item { Id = 3, Name = "Pencil" }
        };

        // GET: api/items
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(items);
        }

        // GET: api/items/1
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                return NotFound("Item not found");
            }

            return Ok(item);
        }

        // GET: api/items/search?name=pen
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("The 'name' query parameter is required.");
            }

            var result = items
                .Where(i => i.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(result);
        }

        // POST: api/items
        [HttpPost]
        public IActionResult Create(Item item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                return BadRequest("Item name is required.");
            }

            int newId = items.Max(i => i.Id) + 1;

            item.Id = newId;

            items.Add(item);

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        // PUT: api/items/5
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Item updatedItem)
        {
            var item = items.FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                return NotFound("Item not found");
            }

            if (string.IsNullOrWhiteSpace(updatedItem.Name))
            {
                return BadRequest("Item name is required.");
            }

            item.Name = updatedItem.Name;

            return Ok(item);
        }

        // DELETE: api/items/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                return NotFound("Item not found");
            }

            items.Remove(item);

            return NoContent();
        }
    }
}