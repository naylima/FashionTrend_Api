using System;
using FashionTrend.Domain.Enums;
using MediatR;

public sealed record CreateRequestRequest (
    Guid ProductId,
    int Quantity
) : IRequest<CreateRequestResponse>;