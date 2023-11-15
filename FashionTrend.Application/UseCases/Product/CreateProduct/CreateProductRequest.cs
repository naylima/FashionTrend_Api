using System;
using MediatR;

public sealed record CreateProductRequest (
	string Name,
	string Description,
    decimal Price,
    List<Guid> MaterialIds
) : IRequest<CreateProductResponse>;