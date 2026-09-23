using FluentValidation;
using Microsoft.EntityFrameworkCore;
using StudentsMVC;
using StudentsMVC.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<StudentContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllersWithViews();

// Register all FluentValidation validators from the Application layer.
builder.Services.AddValidatorsFromAssemblyContaining<CreateStudentValidator>();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Student}/{action=Index}/{id?}");

app.Run();
