using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FashionTrend.Domain.Entities
{
	public class Material : BaseEntity
	{
		public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Supplier> Suppliers { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}

