using System;
using FashionTrend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FashionTrend.Persistence.Context;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<Supplier> Suppliers { get; set; }
	public DbSet<Material> Materials { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Request> Requests { get; set; }
	public DbSet<Payment> Payments { get; set; }
	public DbSet<Contract> Contracts { get; set; }
    public DbSet<MaterialSupplier> MaterialSuppliers { get; set; }
    public DbSet<MaterialProduct> MaterialProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MaterialSupplier>()
           .HasKey(ms => new { ms.SupplierId, ms.MaterialId });

        modelBuilder.Entity<MaterialProduct>()
            .HasKey(mp => new { mp.ProductId, mp.MaterialId });

        modelBuilder.Entity<Request>()
            .HasOne(r => r.Supplier)
            .WithMany(s => s.Requests)
            .HasForeignKey(r => r.SupplierId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Request>()
            .HasOne(r => r.Product)
            .WithMany(p => p.Requests)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Request>()
           .HasOne(r => r.Contract)
           .WithMany(p => p.Requests)
           .HasForeignKey(r => r.ContractId)
           .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Payment>()
           .HasOne(p => p.Contract)
           .WithMany(c => c.Payments)
           .HasForeignKey(p => p.ContractId)
           .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}

