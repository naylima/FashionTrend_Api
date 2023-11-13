using System;
using FashionTrend.Domain.Enums;
using MediatR;

public sealed record GetRequestsByProductIdRequest(
        Guid ProductId
    ) : IRequest<IEnumerable<GetRequestsByProductIdResponse>>;