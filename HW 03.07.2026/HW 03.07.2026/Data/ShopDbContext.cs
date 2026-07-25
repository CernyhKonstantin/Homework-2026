using HW_03_07_2026.Models;
using Microsoft.EntityFrameworkCore;

namespace HW_03_07_2026.Data;

public class ShopDbContext : DbContext
{
    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

    public DbSet<Category> Categories => Set<Category>();

    public override int SaveChanges()
    {
        SetDates();
        return base.SaveChanges();
    }

    private void SetDates()
    {
        var now = DateTime.UtcNow;

        foreach (var e in ChangeTracker.Entries<BaseEntity>())
        {
            if (e.State == EntityState.Added)
            {
                e.Entity.CreatedAt = now;
                e.Entity.UpdatedAt = now;
            }
            if (e.State == EntityState.Modified)
            {
                e.Entity.UpdatedAt = now;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Slug)
            .IsUnique();

        modelBuilder.Entity<Category>()
            .HasOne(c => c.Parent)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(c => c.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}