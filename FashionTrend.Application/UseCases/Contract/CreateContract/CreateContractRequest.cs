using System;
using FashionTrend.Domain.Enums;
using MediatR;

public sealed record CreateContractRequest (
	string ContractNumber,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate,
    ContractStatus Status,
    Guid SupplierId
) : IRequest<CreateContractResponse>;