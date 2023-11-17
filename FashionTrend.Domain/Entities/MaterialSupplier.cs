using System;
using System.Text.Json.Serialization;

namespace FashionTrend.Domain.Entities;

public class MaterialSupplier
{
    public Guid SupplierId { get; set; }
    public Guid MaterialId { get; set; }

    [JsonIgnore]
    public virtual Supplier Supplier { get; set; }
    [JsonIgnore]
    public virtual Material Material { get; set; }
}

