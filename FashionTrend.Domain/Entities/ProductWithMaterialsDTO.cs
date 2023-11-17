using System;
namespace FashionTrend.Domain.Entities;

public class ProductWithMaterialsDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTimeOffset? DateCreated { get; set; }
    public List<string> Materials { get; set; }
}

