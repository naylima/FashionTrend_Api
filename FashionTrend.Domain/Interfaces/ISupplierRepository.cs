using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FashionTrend.Domain.Entities;
namespace FashionTrend.Domain.Interfaces;

public interface ISupplierRepository : IBaseRepository<Supplier>
{
    Task<Supplier> GetByEmail(string email, CancellationToken cancellationToken);
    Task<bool> SupplierHasMaterial(Guid supplierId, Guid materialId, CancellationToken cancellationToken);
    Task<bool> SupplierHasMaterials(Guid supplierId, Guid productId, CancellationToken cancellationToken);
    void AddMaterial(Guid supplierId, Guid materialId, CancellationToken cancellationToken);
}

