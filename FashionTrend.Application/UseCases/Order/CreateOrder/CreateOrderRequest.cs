using System;
using FashionTrend.Domain.Enums;
using MediatR;

public sealed record CreateOrderRequest(
    Guid ProductId,
    int Quantity
) : IRequest<CreateOrderResponse>;