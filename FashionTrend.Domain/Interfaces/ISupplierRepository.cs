using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FashionTrend.Domain.Entities;
namespace FashionTrend.Domain.Interfaces;

public interface ISupplierRepository : IBaseRepository<Supplier>
{
    Task<Supplier> GetByEmail(string email, CancellationToken cancellationToken);
    Task<IEnumerable<Supplier>> GetByMaterial(string materialType, CancellationToken cancellationToken);
}

