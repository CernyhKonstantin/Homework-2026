using HW_05._09._2026.Data;
using HW_05._09._2026.Models;
using Microsoft.EntityFrameworkCore;

namespace HW_05._09._2026.Helpers;

public static class DatabaseSeeder
{
    public static async Task SeedAdminAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        await context.Database.MigrateAsync();

        if (!configuration.GetValue<bool>("AdminSeed:Enabled", true))
            return;

        var adminEmail = configuration["AdminSeed:Email"];
        var adminPassword = configuration["AdminSeed:Password"];

        if (string.IsNullOrWhiteSpace(adminEmail) ||
            string.IsNullOrWhiteSpace(adminPassword))
            return;

        var existingAdmin = await context.Users
            .AnyAsync(x => x.Role == "Admin");

        if (existingAdmin)
            return;

        var admin = new User
        {
            Email = adminEmail.Trim().ToLowerInvariant(),
            Password = BCrypt.Net.BCrypt.HashPassword(adminPassword),
            Role = "Admin",
            CreatedAt = DateTime.UtcNow
        };

        context.Users.Add(admin);
        await context.SaveChangesAsync();
    }
}
