using System;
using FashionTrend.Domain.Enums;
using MediatR;

public sealed record GetOrdersByStatusRequest(
        OrderStatus Status
    ) : IRequest<IEnumerable<GetOrdersByStatusResponse>>;