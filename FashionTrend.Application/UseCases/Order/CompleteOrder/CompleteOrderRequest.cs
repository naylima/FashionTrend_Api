using System;
using MediatR;

public sealed record CompleteOrderRequest (
    Guid Id
    ) : IRequest<CompleteOrderResponse>;