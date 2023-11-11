using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FashionTrend.Persistence.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
	public ProductRepository(AppDbContext context) : base(context)
    {
	}

    public async Task<Product> GetByName(string name, CancellationToken cancellationToken)
    {
        return await context.Products.FirstOrDefaultAsync(
            p => p.Name.Equals(name), cancellationToken);
    }
}

