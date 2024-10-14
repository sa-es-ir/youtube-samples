
using KafkaWithBlockingCollection.Brokers;

namespace KafkaWithBlockingCollection;

public class ConsumerBackgroundService : BackgroundService
{
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<ConsumerBackgroundService> _logger;

    public ConsumerBackgroundService(IMessageBroker messageBroker, ILogger<ConsumerBackgroundService> logger)
    {
        _messageBroker = messageBroker;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        while (!stoppingToken.IsCancellationRequested)
        {
            var message = _messageBroker.Consume<MessageModel>(Constants.TOPIC, TimeSpan.FromSeconds(10));

            if (message is not null)
            {
                _logger.LogInformation("Got a message with Id: {messageId} and Name: {name}", message.Id, message.Name);
            }
            else
                _logger.LogInformation("There is no message for the given topic.");
        }
    }
}
