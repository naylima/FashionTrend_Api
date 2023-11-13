using System;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Enums;

public class GetPaymentsByContractIdResponse
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public DateTimeOffset PaymentDate { get; set; }
}