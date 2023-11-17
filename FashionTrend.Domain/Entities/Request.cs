using System;
using System.Text.Json.Serialization;
using FashionTrend.Domain.Enums;

namespace FashionTrend.Domain.Entities;

public class Request : BaseEntity
{
    public Guid ProductId { get; set; }
    public Guid? SupplierId { get; set; }
    public Guid? ContractId { get; set; }
    public int Quantity { get; set; }
    public RequestStatus Status { get; set; }
    public decimal Value { get; set; }

    public virtual ICollection<Payment> Payments { get; set; }

    [JsonIgnore]
    public Product Product { get; set; }
    [JsonIgnore]
    public virtual Supplier? Supplier { get; set; }
    [JsonIgnore]
    public virtual Contract? Contract { get; set; }
}