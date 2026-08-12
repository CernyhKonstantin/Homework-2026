using HW_10._08._2026.Data;
using HW_10._08._2026.Models;
using HW_10._08._2026.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HW_10._08._2026.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ShopDbContext _context;

    public CategoryRepository(ShopDbContext context) => _context = context;

    public Task<List<Category>> GetAllAsync() =>
        _context.Categories.Include(x => x.Parent).Include(x => x.Children)
            .AsNoTracking().ToListAsync();

    public Task<Category?> GetByIdAsync(int id) =>
        _context.Categories.Include(x => x.Parent).Include(x => x.Children)
            .Include(x => x.Products).ThenInclude(x => x.Images)
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Category> CreateAsync(Category category)
    {
        _context.Categories.Add(category);
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
}
