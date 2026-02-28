using Microsoft.EntityFrameworkCore;
using HW_27_02_2026.Models;

namespace HW_27_02_2026.Context
{
    public class AcademyDbContext : DbContext
    {
        public AcademyDbContext(DbContextOptions<AcademyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Grade> Grades { get; set; }
    }
}