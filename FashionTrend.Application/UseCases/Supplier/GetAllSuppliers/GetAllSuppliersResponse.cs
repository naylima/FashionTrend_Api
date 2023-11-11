using System;
using FashionTrend.Domain.Entities;

public class GetAllSuppliersResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTimeOffset? DateCreated { get; set; }
}