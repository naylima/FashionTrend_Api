using System;
using MediatR;

public sealed record GetActiveContractsRequest() : IRequest<IEnumerable<GetActiveContractsResponse>>;