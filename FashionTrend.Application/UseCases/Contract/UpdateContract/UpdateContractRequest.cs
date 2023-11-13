using System;
using FashionTrend.Domain.Enums;
using MediatR;

public sealed record UpdateContractRequest(
    Guid Id,
    string ContractNumber,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate,
    ContractStatus Status
) : IRequest<UpdateContractResponse>;