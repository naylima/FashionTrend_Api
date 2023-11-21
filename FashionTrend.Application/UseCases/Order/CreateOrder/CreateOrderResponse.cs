using System;
using System.Text.Json.Serialization;
using FashionTrend.Domain.Enums;

public class CreateOrderResponse
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public OrderStatus Status { get; set; }
    public decimal Value { get; set;  }
}

