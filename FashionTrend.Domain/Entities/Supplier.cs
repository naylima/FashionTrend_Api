using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FashionTrend.Domain.Entities;

public class Supplier : BaseEntity
{
	public string Name { get; set; }
	public string Email { get; set; }

    public virtual ICollection<Material> Materials { get; set; }

    [JsonIgnore]
    public virtual ICollection<Request> Requests { get; set; }
}

