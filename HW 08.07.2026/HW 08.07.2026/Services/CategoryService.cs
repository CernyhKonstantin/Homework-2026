using HW_08._07._2026.DTOs.Category;
using HW_08._07._2026.Models;
using HW_08._07._2026.Repositories.Interfaces;
using HW_08._07._2026.Services.Interfaces;

namespace HW_08._07._2026.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryReadDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(MapToReadDto).ToList();
        }

        public async Task<CategoryReadDto?> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return null;

            return MapToReadDto(category);
        }

        public async Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Slug = dto.Slug,
                ParentId = dto.ParentId
            };

            var created = await _categoryRepository.CreateAsync(category);
            return MapToReadDto(created);
        }

        public async Task<bool> UpdateAsync(int id, CategoryUpdateDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return false;

            category.Name = dto.Name;
            category.Slug = dto.Slug;
            category.ParentId = dto.ParentId;

            await _categoryRepository.UpdateAsync(category);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return false;

            await _categoryRepository.DeleteAsync(category);
            return true;
        }

        public async Task<List<CategoryReadDto>> GetParentsByCategoryIdAsync(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
                return new List<CategoryReadDto>();

            var parents = await _categoryRepository.GetParentsByCategoryIdAsync(categoryId);
            return parents.Select(MapToReadDto).ToList();
        }

        public async Task<List<CategoryReadDto>> GetChildrenByCategoryIdAsync(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
                return new List<CategoryReadDto>();

            var children = await _categoryRepository.GetChildrenByCategoryIdAsync(categoryId);
            return children.Select(MapToReadDto).ToList();
        }

        public async Task<List<CategoryTreeDto>> GetCategoryTreeAsync()
        {
            var allCategories = await _categoryRepository.GetAllAsync();

            var rootCategories = allCategories
                .Where(c => c.ParentId == null)
                .ToList();

            return rootCategories
                .Select(root => BuildTree(root, allCategories))
                .ToList();
        }

        private CategoryTreeDto BuildTree(Category category, List<Category> allCategories)
        {
            var children = allCategories
                .Where(c => c.ParentId == category.Id)
                .ToList();

            return new CategoryTreeDto
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug,
                ParentId = category.ParentId,
                Children = children
                    .Select(child => BuildTree(child, allCategories))
                    .ToList()
            };
        }

        private static CategoryReadDto MapToReadDto(Category category)
        {
            return new CategoryReadDto
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug,
                ParentId = category.ParentId
            };
        }
    }
}