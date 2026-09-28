using Microsoft.EntityFrameworkCore;
using StudentsMVC.Models;

namespace StudentsMVC
{
    // The EF Core context represents the application database and exposes its entity sets.
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Category> Categories { get; set; }
        public StudentContext(DbContextOptions<StudentContext> options)
           : base(options)
        {
            if (Database.EnsureCreated())
            {
                Students?.Add(new Student { Name = "Bogdan", Surname = "Ivanenko", Age = 20, GPA = 10.5 });
                Students?.Add(new Student { Name = "Anna", Surname = "Shevchenko", Age = 23, GPA = 11.5 });
                Students?.Add(new Student { Name = "Peter", Surname = "Petrenko", Age = 25, GPA = 12 });
                Categories?.Add(new Category { Name = "Electronics" });
                Categories?.Add(new Category { Name = "Books" });
                SaveChanges();
            }
        }
    }
}