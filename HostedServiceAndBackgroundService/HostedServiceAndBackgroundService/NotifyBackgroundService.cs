namespace HostedServiceAndBackgroundService;

public class NotifyBackgroundService : BackgroundService
{
    private readonly ILogger<NotifyBackgroundService> _logger;

    public NotifyBackgroundService(ILogger<NotifyBackgroundService> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var usersCount = 5;
        while (!stoppingToken.IsCancellationRequested)
        {
            for (var i = 1; i <= usersCount; i++)
            {
                _logger.LogInformation("HostedService: Notifying user {number}", i);

                await Task.Delay(5000);
            }
        }
    }
}
