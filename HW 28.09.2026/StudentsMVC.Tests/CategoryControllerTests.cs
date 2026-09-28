using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsMVC.Controllers;
using StudentsMVC.Models;
using Xunit;

namespace StudentsMVC.Tests;

public class CategoryControllerTests
{
    [Fact]
    public async Task GetById_ReturnsOkWithCategory_WhenCategoryExists()
    {
        await using var context = CreateContext();
        context.Categories.Add(new Category { Id = 100, Name = "Test Category" });
        await context.SaveChangesAsync();

        var controller = new CategoryController(context);

        var result = await controller.GetById(100);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var category = Assert.IsType<Category>(okResult.Value);
        Assert.Equal(100, category.Id);
        Assert.Equal("Test Category", category.Name);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenCategoryDoesNotExist()
    {
        await using var context = CreateContext();
        var controller = new CategoryController(context);

        var result = await controller.GetById(999);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.NotNull(notFoundResult.Value);
    }

    private static StudentContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<StudentContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new StudentContext(options);
    }
}
