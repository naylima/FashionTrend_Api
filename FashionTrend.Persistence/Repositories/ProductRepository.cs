using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Interfaces;
using FashionTrend.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FashionTrend.Persistence.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
	public ProductRepository(AppDbContext context) : base(context)
    {
	}

    public async Task<IEnumerable<Product>> GetByMaterial(string material, CancellationToken cancellationToken)
    {
        return await context.Products
            .Where(s => s.Materials.Any(m => m.Equals(material)))
            .ToListAsync(cancellationToken);
    }
}

