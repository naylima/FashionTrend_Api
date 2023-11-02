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
}

