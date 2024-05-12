using Confluent.Kafka;

namespace KafkaDotNet.ProducerApi;

public class ProducerService
{
    private readonly ILogger<ProducerService> _logger;

    public ProducerService(ILogger<ProducerService> logger)
    {
        _logger = logger;
    }

    public async Task ProduceAsync(CancellationToken cancellationToken)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
            AllowAutoCreateTopics = true,
            Acks = Acks.All
        };

        using var producer = new ProducerBuilder<Null, string>(config).Build();

        try
        {
            var deliveryResult = await producer.ProduceAsync(topic: "test-topic",
                new Message<Null, string>
                {
                    Value = $"Hello, Kafka! {DateTime.UtcNow}"
                },
                cancellationToken);

            _logger.LogInformation($"Delivered message to {deliveryResult.Value}, Offset: {deliveryResult.Offset}");
        }
        catch (ProduceException<Null, string> e)
        {
            _logger.LogError($"Delivery failed: {e.Error.Reason}");
        }

        producer.Flush(cancellationToken);
    }
}
