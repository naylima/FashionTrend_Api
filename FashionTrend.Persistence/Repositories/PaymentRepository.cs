using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FashionTrend.Persistence.Repositories;

public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Payment>> GetByOrderId(Guid orderId, CancellationToken cancellationToken)
    {
        return await context.Payments
            .Where(p => p.OrderId.Equals(orderId))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Payment>> GetByDateRange(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        return await context.Payments
            .Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate)
            .ToListAsync(cancellationToken);
    }
}

