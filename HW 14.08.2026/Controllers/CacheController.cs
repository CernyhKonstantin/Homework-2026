using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace HW_14._08._2026.Controllers;

[ApiController]
[Route("api/cache")]
public class CacheController : ControllerBase
{
    private readonly IDistributedCache _cache;

    public CacheController(IDistributedCache cache)
    {
        _cache = cache;
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
}
