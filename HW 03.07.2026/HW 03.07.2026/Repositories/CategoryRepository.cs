using HW_03_07_2026.Data;
using HW_03_07_2026.Models;
using HW_03_07_2026.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HW_03_07_2026.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ShopDbContext _context;

    public CategoryRepository(ShopDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllAsync()
        => await _context.Categories.ToListAsync();

    public async Task<Category?> GetByIdAsync(int id)
        => await _context.Categories.FindAsync(id);

    public async Task<Category> CreateAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category?> UpdateAsync(Category category)
    {
        var existing = await _context.Categories.FindAsync(category.Id);
        if (existing == null) return null;

        existing.Name = category.Name;
        existing.Slug = category.Slug;
        existing.ParentId = category.ParentId;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Categories.FindAsync(id);
        if (entity == null) return false;

        _context.Categories.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}