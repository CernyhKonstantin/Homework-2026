using HW_10._07._2026.Data;
using HW_10._07._2026.Models;
using HW_10._07._2026.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HW_10._07._2026.Repositories;

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
            .Include(c => c.SubCategories)
            .ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories
            .Include(c => c.SubCategories)
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Category>> GetRootCategoriesAsync()
    {
        return await _context.Categories
            .Where(c => c.ParentId == null)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<List<Category>> GetChildrenAsync(int parentId)
    {
        return await _context.Categories
            .Where(c => c.ParentId == parentId)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<List<Category>> SearchAsync(string name)
    {
        return await _context.Categories
            .Where(c => c.Name.Contains(name))
            .OrderBy(c => c.Name)
            .ToListAsync();
    }
}