using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Enums;

namespace FashionTrend.Domain.Interfaces;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<IEnumerable<Order>> GetBySupplierId(Guid supplierId, CancellationToken cancellationToken);
    Task<IEnumerable<Order>> GetByProductId(Guid productId, CancellationToken cancellationToken);
    Task<IEnumerable<Order>> GetByStatus(OrderStatus status, CancellationToken cancellationToken);
}

