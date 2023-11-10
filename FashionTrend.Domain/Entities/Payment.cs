using System;
using System.Text.Json.Serialization;
using FashionTrend.Domain.Enums;

namespace FashionTrend.Domain.Entities;

public class Payment : BaseEntity
{
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public DateTimeOffset PaymentDate { get; set; }
    public Guid ContractId { get; set; }

    [JsonIgnore]
    public virtual Contract Contract { get; set; }
}

