using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Enums;

namespace FashionTrend.Domain.Interfaces;

public interface IContractRepository : IBaseRepository<Contract>
{
    Task<Contract> GetByContractNumber(string contractNumber, CancellationToken cancellationToken);
    Task<IEnumerable<Contract>> GetContractsByStatus(ContractStatus contractStatus,CancellationToken cancellationToken);
    Task<Contract> GetActiveContractBySupplierId(Guid supplierId, CancellationToken cancellationToken);
    Task<decimal> GetTotalContractValue(Guid contractId, CancellationToken cancellationToken);
}

