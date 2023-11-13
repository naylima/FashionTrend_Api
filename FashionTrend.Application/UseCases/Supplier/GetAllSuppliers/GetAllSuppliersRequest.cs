using System;
using MediatR;

public sealed record GetAllSuppliersRequest () : IRequest<IEnumerable<GetAllSuppliersResponse>>;