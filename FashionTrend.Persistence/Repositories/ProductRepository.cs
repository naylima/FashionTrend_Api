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

    public async Task<IEnumerable<ProductWithMaterialsDTO>> GetWithMaterials(CancellationToken cancellationToken)
    {
        return await context.Products
         .Include(p => p.MaterialProducts)
             .ThenInclude(mp => mp.Material)
         .Select(p => new ProductWithMaterialsDTO
         {
             Id = p.Id,
             Name = p.Name,
             Description = p.Description,
             Price = p.Price,
             DateCreated = p.DateCreated,
             Materials = p.MaterialProducts.Select(mp => mp.Material.Name).ToList()
         })
         .ToListAsync(cancellationToken);
    }

    public async Task<bool> ProductHasMaterial(Guid productId, Guid materialId, CancellationToken cancellationToken)
    {
        return await context.MaterialProducts
            .AnyAsync(mp => mp.ProductId == productId && mp.MaterialId == materialId, cancellationToken);
    }

    public void AddMaterial(Guid productId, Guid materialId, CancellationToken cancellationToken)
    {
        var materialProduct = new MaterialProduct
        {
            ProductId = productId,
            MaterialId = materialId
        };

        context.MaterialProducts.Add(materialProduct);
    }
}

