using System;
using Confluent.Kafka;
using System.Text.Json;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Interfaces;
using FashionTrend.Domain.Enums;

namespace FashionTrend.Persistence.Repositories;

public class KafkaProducer : IKafkaProducer
{
    private readonly IProducer<string, string> _producer;

    public KafkaProducer()
	{
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
        };
        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task<Message> ProduceAsync(string topic, string sender, string receiver, string content)
    {
        var message = new Message
        {
            Id = Guid.NewGuid(),
            Sender = sender,
            Receiver = receiver,
            Content = content,
            Timestamp = DateTime.UtcNow,
            Status = MessageStatus.InProcessing
        };

        string serielizedMessage = JsonSerializer.Serialize(message);

        var deliveryReport = await _producer.ProduceAsync(topic, new Message<string, string>
        {
            Value = serielizedMessage
        });

        if (deliveryReport.Status == PersistenceStatus.NotPersisted)
        {
            message.Status = MessageStatus.WithError;
            return message;
        }
        else
        {
            message.Status = MessageStatus.Successful;
            return message;
        }
    }

    public async Task<Message> ProduceAsyncWithRetry(string topic, string sender,
        string receiver, string content)
    {
        var message = new Message
        {
            Id = Guid.NewGuid(),
            Sender = sender,
            Receiver = receiver,
            Content = content,
            Timestamp = DateTime.UtcNow,
            Status = MessageStatus.InProcessing
        };

        string serielizedMessage = JsonSerializer.Serialize(message);

        int maxRetries = 3;
        int retryIntervalms = 1000;

        for (int attemp = 1; attemp <= maxRetries; attemp++)
        {
            try
            {
                var deliveryReport = await _producer.ProduceAsync(topic, new Message<string, string>
                {
                    Value = serielizedMessage
                });

                message.Status = MessageStatus.Successful;
                break;

            }
            catch (ProduceException<Null, string> ex)
            {
                if (attemp < maxRetries)
                {
                    Thread.Sleep(retryIntervalms);
                    message.Status = MessageStatus.Retry;
                }
                else
                {
                    message.Status = MessageStatus.WithError;
                    throw;
                }
            }
        }
        return message;
    }
}

