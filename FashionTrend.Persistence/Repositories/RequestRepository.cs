using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Enums;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FashionTrend.Persistence.Repositories;

public class RequestRepository : BaseRepository<Request>, IRequestRepository
{
	public RequestRepository(AppDbContext context) : base(context)
    {
	}

    public async Task<IEnumerable<Request>> GetByProductId(Guid productId, CancellationToken cancellationToken)
    {
        return await context.Requests
            .Where(r => r.ProductId.Equals(productId))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Request>> GetByStatus(RequestStatus status, CancellationToken cancellationToken)
    {
        return await context.Requests
            .Include(r => r.Payments)
            .Where(r => r.Status.Equals(status))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Request>> GetBySupplierId(Guid supplierId, CancellationToken cancellationToken)
    {
        return await context.Requests
            .Include(r => r.Payments)
            .Where(r => r.SupplierId.Equals(supplierId))
            .ToListAsync(cancellationToken);
    }
}

