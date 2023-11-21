using System;
using FashionTrend.Domain.Enums;

public class CreateMessageResponse
{
    public Guid Id { get; set; }
    public string Sender { get; set; }
    public string Receiver { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public MessageStatus Status { get; set; }
}

