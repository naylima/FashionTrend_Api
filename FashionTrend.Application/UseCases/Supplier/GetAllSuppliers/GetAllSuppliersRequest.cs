using System;
using MediatR;

public sealed record GetAllSuppliersRequest () : IRequest<GetAllSuppliersResponse>;