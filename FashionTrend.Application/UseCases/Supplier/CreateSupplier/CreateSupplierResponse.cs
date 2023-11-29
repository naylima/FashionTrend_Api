using System;

public class CreateSupplierResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTimeOffset? DateCreated { get; set; }
}

