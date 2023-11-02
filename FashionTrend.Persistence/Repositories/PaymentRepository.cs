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

public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Payment>> GetPaymentsByDateRange(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        return await context.Payments
            .Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Payment>> GetPaymentsByRequestId(Guid requestId, CancellationToken cancellationToken)
    {
        return await context.Payments
            .Where(p => p.RequestId.Equals(requestId))
            .ToListAsync(cancellationToken);
    }

    public async Task<decimal> GetTotalPaymentsByRequestId(Guid requestId, CancellationToken cancellationToken)
    {
        return await context.Payments
            .Where(p => p.RequestId.Equals(requestId))
            .SumAsync(p => p.Amount, cancellationToken);
    }
}

