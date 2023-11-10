using System;
namespace FashionTrend.Application.UseCases.Suppliers.CreateSupplier;

public class CreateSupplierResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTimeOffset? DateCreated { get; set; }
}

