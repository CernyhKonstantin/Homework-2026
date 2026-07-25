using HW_01_07_2026.Data;
using HW_01_07_2026.Models;
using HW_01_07_2026.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HW_01_07_2026.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ShopDbContext _context;

    public CategoryRepository(ShopDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category> CreateAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category?> UpdateAsync(Category category)
    {
        var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);

        if (existingCategory == null)
            return null;

        existingCategory.Name = category.Name;
        existingCategory.Slug = category.Slug;
        existingCategory.ParentId = category.ParentId;

        await _context.SaveChangesAsync();
        return existingCategory;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
            return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}