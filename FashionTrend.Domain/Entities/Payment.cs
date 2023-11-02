using System;
using System.Text.Json.Serialization;

namespace FashionTrend.Domain.Entities;

public class Payment : BaseEntity
{
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; }
    public DateTimeOffset PaymentDate { get; set; }
    public Guid? RequestId { get; set; }

    [JsonIgnore]
    public virtual Request Request { get; set; }
}

