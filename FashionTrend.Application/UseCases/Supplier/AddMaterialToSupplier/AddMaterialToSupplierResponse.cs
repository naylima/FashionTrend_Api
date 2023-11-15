using System;

public class AddMaterialToSupplierResponse
{
    public Guid Id { get; set; }
    public Guid MaterialId { get; set; }
    public Guid SupplierId { get; set; }
    public DateTimeOffset? DateCreated { get; set; }
}