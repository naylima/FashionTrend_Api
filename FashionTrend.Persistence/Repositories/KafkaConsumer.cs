using Confluent.Kafka;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Interfaces;

public class KafkaConsumer : IKafkaConsumer
{
    private bool isConsuming = false;

    public event EventHandler<MessageReceivedEventArgs> OnMessageReceived;

    private IConsumer<Ignore, string> consumer;

    public async Task StartConsumingAsync(CancellationToken cancellationToken)
    {
        isConsuming = true;
        while (isConsuming)
        {
            try
            {
                var consumeResult = await Task.Run(() => consumer.Consume(cancellationToken), cancellationToken);
                if (consumeResult != null && consumeResult.Message != null)
                {
                    string message = consumeResult.Message.Value;
                    OnMessageReceived?.Invoke(this, new MessageReceivedEventArgs { Message = message });
                }
                StopConsuming();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while consuming the Kafka topic. See inner exception for details.", ex);
            }
        }
    }

    public void StopConsuming()
    {
        isConsuming = false;
        consumer.Close();
    }

    public void Subscribe(string topic, string group)
    {
        // configurar a string de conexão com o Kafka
        // configura~]ao do servidor local do cluster do kafka
        try
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = group,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };
            consumer = new ConsumerBuilder<Ignore, string>(config).Build();

            consumer.Subscribe(topic);
        }
        catch (Exception ex)
        {
            throw new Exception("Error subscribing to the Kafka topic. See inner exception for details.", ex);
        }
    }
}