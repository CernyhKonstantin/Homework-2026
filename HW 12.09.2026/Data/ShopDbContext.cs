using HW_12._09._2026.Models;
using Microsoft.EntityFrameworkCore;

namespace HW_12._09._2026.Data;

public class ShopDbContext : DbContext
{
    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<PasswordResetToken> PasswordResetTokens => Set<PasswordResetToken>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
    public DbSet<UserAddress> UserAddresses => Set<UserAddress>();
    public DbSet<Provider> Providers => Set<Provider>();
    public DbSet<UserProvider> UserProviders => Set<UserProvider>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .Property(x => x.Role)
            .HasMaxLength(30)
            .IsRequired();

        modelBuilder.Entity<RefreshToken>()
            .HasIndex(x => x.Token)
            .IsUnique();

        modelBuilder.Entity<RefreshToken>()
            .HasOne(x => x.User)
            .WithMany(x => x.RefreshTokens)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PasswordResetToken>()
            .HasIndex(x => x.Token)
            .IsUnique();

        modelBuilder.Entity<PasswordResetToken>()
            .HasOne(x => x.User)
            .WithMany(x => x.PasswordResetTokens)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Category>()
            .HasOne(x => x.Parent)
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>()
            .HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProductImage>()
            .HasOne(x => x.Product)
            .WithMany(x => x.Images)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>()
            .HasOne(x => x.User)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserAddress>()
            .HasOne(x => x.User)
            .WithMany(x => x.Addresses)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(x => x.Order)
            .WithMany(x => x.Details)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Provider>()
            .HasIndex(x => x.Name)
            .IsUnique();

        modelBuilder.Entity<Provider>()
            .Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<UserProvider>()
            .Property(x => x.NumberProvider)
            .HasColumnName("number_provider")
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<UserProvider>()
            .Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        modelBuilder.Entity<UserProvider>()
            .Property(x => x.UserId)
            .HasColumnName("user_id");

        modelBuilder.Entity<UserProvider>()
            .Property(x => x.ProviderId)
            .HasColumnName("provider_id");

        modelBuilder.Entity<UserProvider>()
            .HasIndex(x => new { x.UserId, x.ProviderId })
            .IsUnique();

        modelBuilder.Entity<UserProvider>()
            .HasIndex(x => new { x.ProviderId, x.NumberProvider })
            .IsUnique();

        modelBuilder.Entity<UserProvider>()
            .HasOne(x => x.User)
            .WithMany(x => x.UserProviders)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserProvider>()
            .HasOne(x => x.Provider)
            .WithMany(x => x.UserProviders)
            .HasForeignKey(x => x.ProviderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
