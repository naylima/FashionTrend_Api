using System;
using MediatR;

public sealed record GetAllRequestsRequest() : IRequest<IEnumerable<GetAllRequestsResponse>>;