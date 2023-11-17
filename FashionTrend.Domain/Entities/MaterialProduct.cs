using System;
using System.Text.Json.Serialization;

namespace FashionTrend.Domain.Entities;

public class MaterialProduct
{
    public Guid ProductId { get; set; }
    public Guid MaterialId { get; set; }

    [JsonIgnore]
    public virtual Product Product { get; set; }
    [JsonIgnore]
    public virtual Material Material { get; set; }
}

