using System;
using FashionTrend.Domain.Enums;
using MediatR;

public sealed record CreatePaymentRequest (
    decimal Amount,
    PaymentMethod PaymentMethod,
    DateTimeOffset PaymentDate,
    Guid OrderId
) : IRequest<CreatePaymentResponse>;