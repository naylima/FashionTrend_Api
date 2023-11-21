using System;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Enums;

public class GetContractResponse
{
    public Guid Id { get; set; }
    public string ContractNumber { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public ContractStatus Status { get; set; }
    public decimal TotalValue { get; set; }

    public ICollection<Order> Orders { get; set; }
}