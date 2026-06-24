using HW_22_06_2026.DTOs;
using HW_22_06_2026.Models;

namespace HW_22_06_2026.Services;

public class ProductService : IProductService
{
    private readonly List<Product> products = new()
    {
        new Product { Id = 1, Name = "Laptop", Price = 1200m },
        new Product { Id = 2, Name = "Phone", Price = 800m },
        new Product { Id = 3, Name = "Keyboard", Price = 50m }
    };

    public List<Product> GetAll()
    {
        return products;
    }

    public Product? GetById(int id)
    {
        return products.FirstOrDefault(p => p.Id == id);
    }

    public Product Create(CreateProductDto dto)
    {
        var product = new Product
        {
            Id = products.Count == 0 ? 1 : products.Max(x => x.Id) + 1,
            Name = dto.Name,
            Price = dto.Price
        };

        products.Add(product);
        return product;
    }

    public Product? Update(int id, UpdateProductDto dto)
    {
        var product = GetById(id);

        if (product == null)
            return null;

        product.Name = dto.Name;
        product.Price = dto.Price;

        return product;
    }

    public bool Delete(int id)
    {
        var product = GetById(id);

        if (product == null)
            return false;

        products.Remove(product);
        return true;
    }

    public List<Product> Search(string name)
    {
        return products
            .Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}