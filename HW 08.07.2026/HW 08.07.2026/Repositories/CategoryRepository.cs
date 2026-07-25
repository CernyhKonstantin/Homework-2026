using HW_08._07._2026.Data;
using HW_08._07._2026.Models;
using HW_08._07._2026.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HW_08._07._2026.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShopDbContext _context;

        public CategoryRepository(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetParentsByCategoryIdAsync(int categoryId)
        {
            var result = new List<Category>();

            var current = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            while (current != null && current.ParentId.HasValue)
            {
                var parent = await _context.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == current.ParentId.Value);

                if (parent == null)
                    break;

                result.Add(parent);
                current = parent;
            }

            return result;
        }

        public async Task<List<Category>> GetChildrenByCategoryIdAsync(int categoryId)
        {
            return await _context.Categories
                .AsNoTracking()
                .Where(c => c.ParentId == categoryId)
                .ToListAsync();
        }
    }
}