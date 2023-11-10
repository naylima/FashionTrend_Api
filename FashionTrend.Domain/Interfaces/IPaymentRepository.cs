﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FashionTrend.Domain.Entities;

namespace FashionTrend.Domain.Interfaces;

public interface IPaymentRepository : IBaseRepository<Payment>
{
    Task<IEnumerable<Payment>> GetPaymentsByDateRange(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
}

