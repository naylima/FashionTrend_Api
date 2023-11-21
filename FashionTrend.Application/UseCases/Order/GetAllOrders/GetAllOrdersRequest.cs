using System;
using MediatR;

public sealed record GetAllOrdersRequest() : IRequest<IEnumerable<GetAllOrdersResponse>>;