using System;
using System.Text.Json.Serialization;
using FashionTrend.Domain.Enums;

namespace FashionTrend.Domain.Entities;

public class Payment : BaseEntity
{
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public DateTimeOffset PaymentDate { get; set; }
    public Guid? RequestId { get; set; }

    [JsonIgnore]
    public virtual Request Request { get; set; }
}

