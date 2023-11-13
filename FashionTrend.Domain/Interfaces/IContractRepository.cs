﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FashionTrend.Domain.Entities;

namespace FashionTrend.Domain.Interfaces;

public interface IContractRepository : IBaseRepository<Contract>
{
    Task<Contract> GetByContractNumber(string contractNumber, CancellationToken cancellationToken);
    Task<IEnumerable<Contract>> GetActiveContracts(CancellationToken cancellationToken);
    Task<decimal> GetTotalContractValue(Guid contractId, CancellationToken cancellationToken);
}

