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
    public Guid SupplierId { get; set; }

    public virtual ICollection<Payment> Payments { get; set; }

    public decimal TotalValue => Payments.Sum(p => p.Amount);

    [JsonIgnore]
    public virtual Supplier Supplier { get; set; }
}

