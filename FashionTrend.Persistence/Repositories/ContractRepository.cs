﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Enums;
using FashionTrend.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FashionTrend.Persistence.Repositories;

public class ContractRepository : BaseRepository<Contract>, IContractRepository
{
    public ContractRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Contract>> GetContractsByStatus(ContractStatus contractStatus, CancellationToken cancellationToken)
    {
        return await context.Contracts
            .Include(c => c.Orders)
            .Where(c => c.Status.Equals(contractStatus))
            .ToListAsync(cancellationToken);
    }

    public async Task<Contract> GetByContractNumber(string contractNumber, CancellationToken cancellationToken)
    {
        return await context.Contracts
            .Include(c => c.Orders)
            .Where(c => c.ContractNumber.Equals(contractNumber))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Contract> GetActiveContractBySupplierId(Guid supplierId, CancellationToken cancellationToken)
    {
        return await context.Contracts
            .Include(c => c.Orders)
            .Where(c => c.SupplierId.Equals(supplierId) && c.Status == ContractStatus.Active)
            .FirstOrDefaultAsync(cancellationToken);
    }


    public async Task<decimal> GetTotalContractValue(Guid contractId, CancellationToken cancellationToken)
    {
        return await context.Contracts
            .Include(c => c.Orders)
            .Where(c => c.Id.Equals(contractId))
            .Select(c => c.TotalValue)
            .FirstOrDefaultAsync(cancellationToken);
    }
}

