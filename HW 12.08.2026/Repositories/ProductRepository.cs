using HW_10._08._2026.Data;
using HW_10._08._2026.Models;
using HW_10._08._2026.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HW_10._08._2026.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ShopDbContext _context;

    public ProductRepository(ShopDbContext context) => _context = context;

    public Task<List<Product>> GetAllAsync() =>
        _context.Products.Include(x => x.Category).Include(x => x.Images)
            .AsNoTracking().ToListAsync();

    public Task<Product?> GetByIdAsync(int id) =>
        _context.Products.Include(x => x.Category).Include(x => x.Images)
            .FirstOrDefaultAsync(x => x.Id == id);

    public Task<List<Product>> GetByCategoryIdAsync(int categoryId) =>
        _context.Products.Include(x => x.Category).Include(x => x.Images)
            .Where(x => x.CategoryId == categoryId)
            .AsNoTracking().ToListAsync();

    public async Task<Product> CreateAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}
