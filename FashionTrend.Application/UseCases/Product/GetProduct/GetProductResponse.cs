using System;
using FashionTrend.Domain.Entities;

public class GetProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTimeOffset? DateCreated { get; set; }
}