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

public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
{
	public SupplierRepository(AppDbContext context) : base(context)
	{
	}

	public async Task<Supplier> GetByEmail(string email, CancellationToken cancellationToken)
	{
		return await context.Suppliers.FirstOrDefaultAsync(
            s => s.Email.Equals(email), cancellationToken);
	}

	public async Task<IEnumerable<Supplier>> GetByMaterial(string material, CancellationToken cancellationToken)
	{
        return await context.Suppliers
            .Where(s => s.Materials.Any(m => m.Equals(material)))
            .ToListAsync(cancellationToken);
    }
}

