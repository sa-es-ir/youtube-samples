using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KafkaDotNet.Consumer;

internal class EventConsumerJob : BackgroundService
{
    private readonly ILogger<EventConsumerJob> _logger;

    public EventConsumerJob(ILogger<EventConsumerJob> logger)
    {
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {

        return Task.CompletedTask;
    }
}
