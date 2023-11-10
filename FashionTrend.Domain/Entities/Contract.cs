using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using FashionTrend.Domain.Enums;

namespace FashionTrend.Domain.Entities;

public class Contract : BaseEntity
{
    public string ContractNumber { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public ContractStatus Status { get; set; }

    public Contract()
    {
        Payments = new List<Payment>();
    }

    public decimal TotalValue => Payments.Sum(p => p.Amount);

    [JsonIgnore]
    public virtual ICollection<Request> Requests { get; set; }
    [JsonIgnore]
    public virtual ICollection<Payment> Payments { get; set; }
}

