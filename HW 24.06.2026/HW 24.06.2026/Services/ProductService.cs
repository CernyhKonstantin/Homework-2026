using HW_24_06_2026.DTOs;
using HW_24_06_2026.Models;

namespace HW_24_06_2026.Services;

/// <summary>In-memory product service implementation.</summary>
public class ProductService : IProductService
{
    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Laptop", Price = 1200 },
        new Product { Id = 2, Name = "Phone", Price = 800 },
        new Product { Id = 3, Name = "Keyboard", Price = 50 }
    };

    public List<Product> GetAll() => _products;

    public Product? GetById(int id) =>
        _products.FirstOrDefault(p => p.Id == id);

    public Product Create(CreateProductDto dto)
    {
        var product = new Product
        {
            Id = _products.Any() ? _products.Max(x => x.Id) + 1 : 1,
            Name = dto.Name,
            Price = dto.Price
        };

        _products.Add(product);
        return product;
    }

    public Product? Update(int id, UpdateProductDto dto)
    {
        var product = GetById(id);
        if (product == null) return null;

        product.Name = dto.Name;
        product.Price = dto.Price;

        return product;
    }

    public bool Delete(int id)
    {
        var product = GetById(id);
        if (product == null) return false;

        _products.Remove(product);
        return true;
    }

    public List<Product> Search(string name)
    {
        return _products
            .Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}