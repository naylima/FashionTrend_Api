using System;
using FashionTrend.Domain.Enums;

public class CreateContractResponse
{
    public Guid Id { get; set; }
    public string ContractNumber { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public ContractStatus Status { get; set; }
}

