using System;
using FashionTrend.Domain.Enums;

public class CreateRequestResponse
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public RequestStatus Status { get; set; }

    public decimal Value { get; }
}

