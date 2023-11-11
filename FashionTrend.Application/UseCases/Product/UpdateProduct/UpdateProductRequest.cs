using System;
using MediatR;

public sealed record UpdateProductRequest (
    Guid Id,
    string Name,
    string Description,
    decimal Price
) : IRequest<UpdateProductResponse>;