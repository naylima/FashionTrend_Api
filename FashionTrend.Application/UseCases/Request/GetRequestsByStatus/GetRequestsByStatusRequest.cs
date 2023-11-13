using System;
using FashionTrend.Domain.Enums;
using MediatR;

public sealed record GetRequestsByStatusRequest(
        RequestStatus Status
    ) : IRequest<IEnumerable<GetRequestsByStatusResponse>>;