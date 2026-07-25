using HW_22_06_2026.DTOs;
using HW_22_06_2026.Models;

namespace HW_22_06_2026.Services;

public interface IProductService
{
    List<Product> GetAll();
    Product? GetById(int id);
    Product Create(CreateProductDto dto);
    Product? Update(int id, UpdateProductDto dto);
    bool Delete(int id);
    List<Product> Search(string name);
}