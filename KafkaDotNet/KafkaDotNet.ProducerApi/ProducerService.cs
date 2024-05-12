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

    }
}
