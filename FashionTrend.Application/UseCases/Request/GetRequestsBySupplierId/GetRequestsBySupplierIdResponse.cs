using System;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Enums;

public class GetRequestsBySupplierIdResponse
{
    public Guid? SupplierId { get; set; }
    public Guid ProductId { get; set; }
    public Guid? ContractId { get; set; }
    public int Quantity { get; set; }
    public RequestStatus Status { get; set; }

    public decimal Value { get; }

    public ICollection<Payment> Payments { get; set; }
}