using System;
using FashionTrend.Domain.Enums;
using MediatR;

public sealed record GetContractsByStatusRequest(
    ContractStatus contractStatus
    ) : IRequest<IEnumerable<GetContractsByStatusResponse>>;