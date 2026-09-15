using HW_14._09._2026.Services.Interfaces;
using HW_14._09._2026.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HW_14._09._2026.Controllers;

public sealed class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllAsync();
        return View(new CategoriesPageViewModel { Categories = categories });
    }
}
