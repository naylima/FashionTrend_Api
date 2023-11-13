using System;
using MediatR;

public sealed record GetAllContractsRequest() : IRequest<IEnumerable<GetAllContractsResponse>>;