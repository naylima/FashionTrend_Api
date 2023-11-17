using System;
using FashionTrend.Domain.Enums;

public class CreatePaymentResponse
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public DateTimeOffset PaymentDate { get; set; }
    public Guid RequestId { get; set; }
}

