using System;
using MediatR;

public sealed record CreateProductRequest (
	string Name,
	string Description,
    decimal Price
) : IRequest<CreateProductResponse>;