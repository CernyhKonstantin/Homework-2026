using HW_06._07._2026.DTOs.Category;

namespace HW_06._07._2026.Services.Interfaces;

public interface ICategoryService
{
    Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto);
    Task<List<CategoryReadDto>> GetAllAsync();
    Task<CategoryReadDto?> GetByIdAsync(int id);
}