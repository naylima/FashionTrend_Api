using System;
using MediatR;

public sealed record GetAllProductsRequest () : IRequest<IEnumerable<GetAllProductsResponse>>;