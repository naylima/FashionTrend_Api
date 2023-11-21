using System;
using FashionTrend.Domain.Enums;
using MediatR;

public sealed record GetOrdersByProductIdRequest(
        Guid ProductId
    ) : IRequest<IEnumerable<GetOrdersByProductIdResponse>>;