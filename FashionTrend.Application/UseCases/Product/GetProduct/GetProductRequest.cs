using System;
using MediatR;

public sealed record GetProductRequest (
    Guid Id
    ) : IRequest<GetProductResponse>;