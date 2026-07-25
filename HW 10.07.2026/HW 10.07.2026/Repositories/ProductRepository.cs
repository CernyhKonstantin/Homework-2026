using HW_10._07._2026.Data;
using HW_10._07._2026.Models;
using HW_10._07._2026.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HW_10._07._2026.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ShopDbContext _context;

    public ProductRepository(ShopDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetByCategoryIdAsync(int categoryId)
    {
        return await _context.Products
            .Include(p => p.Images)
            .Where(p => p.CategoryId == categoryId)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }
}