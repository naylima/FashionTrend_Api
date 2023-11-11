using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FashionTrend.Domain.Entities;

namespace FashionTrend.Domain.Interfaces;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<Product> GetByName(string name, CancellationToken cancellationToken);
}

