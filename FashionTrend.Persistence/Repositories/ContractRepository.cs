using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Enums;
using FashionTrend.Domain.Interfaces;
using FashionTrend.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FashionTrend.Persistence.Repositories;

public class ContractRepository : BaseRepository<Contract>, IContractRepository
{
    public ContractRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Contract>> GetActiveContracts(CancellationToken cancellationToken)
    {
        return await context.Contracts
            .Where(c => c.Status.Equals(ContractStatus.Active))
            .ToListAsync(cancellationToken);
    }

    public async Task<Contract> GetContractByContractNumber(string contractNumber, CancellationToken cancellationToken)
    {
        return await context.Contracts
            .Where(c => c.ContractNumber.Equals(contractNumber))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Contract>> GetContractsBySupplierId(Guid supplierId, CancellationToken cancellationToken)
    {
        return await context.Contracts
            .Where(c => c.SupplierId.Equals(supplierId))
            .ToListAsync(cancellationToken);
    }

    public async Task<decimal> GetTotalContractValue(Guid contractId, CancellationToken cancellationToken)
    {
        return await context.Contracts
            .Where(c => c.Id.Equals(contractId))
            .Select(c => c.TotalValue)
            .FirstOrDefaultAsync(cancellationToken);
    }
}

