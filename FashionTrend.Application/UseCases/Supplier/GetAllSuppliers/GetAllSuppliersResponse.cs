using System;
using FashionTrend.Domain.Entities;

public class GetAllSuppliersResponse
{
	public IEnumerable<Supplier> Suppliers { get; set; }
}