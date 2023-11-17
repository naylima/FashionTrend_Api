using System;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Enums;

public class GetAllContractsResponse
{
    public Guid Id { get; set; }
    public string ContractNumber { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public ContractStatus Status { get; set; }
    public decimal TotalValue { get; set; }

    public ICollection<Request> Requests { get; set; }
    public ICollection<Payment> Payments { get; set; }
}