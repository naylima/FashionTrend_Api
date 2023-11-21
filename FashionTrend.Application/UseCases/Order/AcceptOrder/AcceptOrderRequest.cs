using System;
using MediatR;

public sealed record AcceptOrderRequest(
    Guid Id,
    Guid SupplierId
) : IRequest<AcceptOrderResponse>;