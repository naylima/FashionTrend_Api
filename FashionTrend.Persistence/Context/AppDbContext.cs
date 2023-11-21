using System;
using FashionTrend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<Supplier> Suppliers { get; set; }
	public DbSet<Material> Materials { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
	public DbSet<Payment> Payments { get; set; }
	public DbSet<Contract> Contracts { get; set; }
    public DbSet<MaterialSupplier> MaterialSuppliers { get; set; }
    public DbSet<MaterialProduct> MaterialProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .Property(r => r.SupplierId)
            .HasDefaultValue(null);

        modelBuilder.Entity<Order>()
            .Property(r => r.ContractId)
            .HasDefaultValue(null);

        modelBuilder.Entity<MaterialSupplier>()
           .HasKey(ms => new { ms.SupplierId, ms.MaterialId });

        modelBuilder.Entity<MaterialProduct>()
            .HasKey(mp => new { mp.ProductId, mp.MaterialId });

        modelBuilder.Entity<Order>()
            .HasOne(r => r.Supplier)
            .WithMany(s => s.Orders)
            .HasForeignKey(r => r.SupplierId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>()
            .HasOne(r => r.Product)
            .WithMany(p => p.Orders)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>()
           .HasOne(r => r.Contract)
           .WithMany(p => p.Orders)
           .HasForeignKey(r => r.ContractId)
           .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Contract>()
           .HasOne(c => c.Supplier)
           .WithMany(s => s.Contracts)
           .HasForeignKey(c => c.SupplierId)
           .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Payment>()
           .HasOne(p => p.Order)
           .WithMany(c => c.Payments)
           .HasForeignKey(p => p.OrderId)
           .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}

