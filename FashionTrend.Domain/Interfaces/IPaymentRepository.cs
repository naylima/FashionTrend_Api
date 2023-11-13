using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FashionTrend.Domain.Entities;

namespace FashionTrend.Domain.Interfaces;

public interface IPaymentRepository : IBaseRepository<Payment>
{
    Task<IEnumerable<Payment>> GetByContractId(Guid contractId, CancellationToken cancellationToken);
    Task<IEnumerable<Payment>> GetByDateRange(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
}

