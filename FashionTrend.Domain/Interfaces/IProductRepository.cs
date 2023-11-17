using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FashionTrend.Domain.Entities;

namespace FashionTrend.Domain.Interfaces;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<Product> GetByName(string name, CancellationToken cancellationToken);
    Task<IEnumerable<ProductWithMaterialsDTO>> GetWithMaterials(CancellationToken cancellationToken);
    Task<bool> ProductHasMaterial(Guid productId, Guid materialId, CancellationToken cancellationToken);
    void AddMaterial(Guid productId, Guid materialId, CancellationToken cancellationToken);
}

