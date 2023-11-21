using System;
using System.Linq;
using System.Collections.Generic;
using FashionTrend.Domain.Enums;
using System.Text.Json.Serialization;

namespace FashionTrend.Domain.Entities;

public class Contract : BaseEntity
{
    public string ContractNumber { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public ContractStatus Status { get; set; }
    public Guid SupplierId { get; set; }

    public Contract ()
    {
        Orders = new List<Order>();
    }
    public decimal TotalValue => Orders.Sum(r => r.Value);

    public virtual ICollection<Order> Orders { get; set; }

    [JsonIgnore]
    public virtual Supplier Supplier { get; set; }
}

