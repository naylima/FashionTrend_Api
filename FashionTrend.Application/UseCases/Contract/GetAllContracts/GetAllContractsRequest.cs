using System;
using MediatR;

public sealed record GetAllContractsRequest() : IRequest<List<GetAllContractsResponse>>;