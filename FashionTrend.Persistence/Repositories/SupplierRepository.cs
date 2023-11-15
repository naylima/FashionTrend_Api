using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Interfaces;
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

    public async Task<bool> SupplierHasMaterial(Guid supplierId, Guid materialId, CancellationToken cancellationToken)
    {
        return await context.MaterialSuppliers
            .AnyAsync(ms => ms.SupplierId == supplierId && ms.MaterialId == materialId, cancellationToken);
    }

    public async Task<bool> SupplierHasMaterials(Guid supplierId, Guid productId, CancellationToken cancellationToken)
    {
        return await context.MaterialSuppliers
            .AnyAsync(ms => ms.SupplierId == supplierId &&
                            context.MaterialProducts.Any(mp => mp.ProductId == productId && mp.MaterialId == ms.MaterialId),
                cancellationToken);
    }

    public void AddMaterial(Guid supplierId, Guid materialId, CancellationToken cancellationToken)
    {
        var materialSupplier = new MaterialSupplier
        {
            SupplierId = supplierId,
            MaterialId = materialId
        };

        context.MaterialSuppliers.Add(materialSupplier);
    }
}

