using System;
using FashionTrend.Domain.Entities;

namespace FashionTrend.Domain.Interfaces;

public interface IKafkaProducer
{
    Task<Message> ProduceAsync(string topic, string sender, string receiver, string content);
    Task<Message> ProduceAsyncWithRetry(string topic, string sender, string receiver, string content);
}

