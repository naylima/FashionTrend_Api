using System;
using System.Text.Json.Serialization;

namespace FashionTrend.Domain.Entities;

public class MaterialProduct : BaseEntity
{
    public Guid ProductId { get; set; }
    public Guid MaterialId { get; set; }

    [JsonIgnore]
    public Product Product { get; set; }
    [JsonIgnore]
    public Material Material { get; set; }
}

