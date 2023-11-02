using System;
using System.Text.Json.Serialization;
using FashionTrend.Domain.Enums;

namespace FashionTrend.Domain.Entities;

public class Request : BaseEntity
{
    public Guid? SupplierId { get; set; } 
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public RequestStatus Status { get; set; }

    [JsonIgnore]
    public virtual Supplier? Supplier { get; set; }
    [JsonIgnore]
    public virtual Product Product { get; set; }
}