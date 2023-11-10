using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FashionTrend.Domain.Entities
{
	public class Material : BaseEntity
	{
		public string Name { get; set; }
        public string Color { get; set; }
    }
}

