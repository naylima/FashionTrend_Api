using System;
using FashionTrend.Domain.Entities;

namespace FashionTrend.Domain.Interfaces;

public interface IKafkaConsumer
{
    event EventHandler<MessageReceivedEventArgs> OnMessageReceived;
    void Subscribe(string topic, string group);
    void StopConsuming();
    Task StartConsumingAsync(CancellationToken cancellationToken);
}

