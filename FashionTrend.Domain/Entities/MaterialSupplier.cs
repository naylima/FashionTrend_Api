using System;
using System.Text.Json.Serialization;

namespace FashionTrend.Domain.Entities;

public class MaterialSupplier : BaseEntity
{
    public Guid SupplierId { get; set; }
    public Guid MaterialId { get; set; }

    [JsonIgnore]
    public Supplier Supplier { get; set; }
    [JsonIgnore]
    public Material Material { get; set; }
}

