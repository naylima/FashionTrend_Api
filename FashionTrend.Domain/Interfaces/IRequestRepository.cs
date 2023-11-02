using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Enums;

namespace FashionTrend.Domain.Interfaces;

public interface IRequestRepository : IBaseRepository<Request>
{
    Task<IEnumerable<Request>> GetBySupplier(Guid supplierId, CancellationToken cancellationToken);
    Task<IEnumerable<Request>> GetByProduct(Guid productId, CancellationToken cancellationToken);
    Task<IEnumerable<Request>> GetByStatus(RequestStatus status, CancellationToken cancellationToken);
}

