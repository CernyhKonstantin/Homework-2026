using HW_19._08._2026.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace HW_19._08._2026.Controllers;

[ApiController]
[Route("api/v1/cache")]
public class CacheController : ControllerBase
{
    private readonly IDistributedCache _cache;
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public CacheController(
        IDistributedCache cache,
        IProductService productService,
        ICategoryService categoryService)
    {
        _cache = cache;
        _productService = productService;
        _categoryService = categoryService;
    }

    [HttpGet("redis")]
    public async Task<IActionResult> TestRedis()
    {
        const string key = "redis:health";

        await _cache.SetStringAsync(
            key,
            "Redis connection works.",
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            });

        var value = await _cache.GetStringAsync(key);

        return Ok(new
        {
            redis = value,
            status = "connected"
        });
    }

    [HttpGet("products/{id:int}")]
    public async Task<IActionResult> TestProductCache(int id)
    {
        var cacheKey = $"products:id:{id}";
        var before = await _cache.GetStringAsync(cacheKey);

        var product = await _productService.GetByIdAsync(id);

        var after = await _cache.GetStringAsync(cacheKey);

        if (product is null)
            return NotFound(new
            {
                message = "Product not found.",
                cacheKey,
                wasCachedBeforeRequest = before is not null,
                wasCachedAfterRequest = after is not null
            });

        return Ok(new
        {
            product,
            cacheKey,
            wasCachedBeforeRequest = before is not null,
            wasCachedAfterRequest = after is not null,
            cacheWasCreatedByRequest = before is null && after is not null
        });
    }

    [HttpGet("categories/{id:int}")]
    public async Task<IActionResult> TestCategoryCache(int id)
    {
        var cacheKey = $"categories:id:{id}";
        var before = await _cache.GetStringAsync(cacheKey);

        var category = await _categoryService.GetByIdAsync(id);

        var after = await _cache.GetStringAsync(cacheKey);

        if (category is null)
            return NotFound(new
            {
                message = "Category not found.",
                cacheKey,
                wasCachedBeforeRequest = before is not null,
                wasCachedAfterRequest = after is not null
            });

        return Ok(new
        {
            category,
            cacheKey,
            wasCachedBeforeRequest = before is not null,
            wasCachedAfterRequest = after is not null,
            cacheWasCreatedByRequest = before is null && after is not null
        });
    }
}
