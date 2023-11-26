namespace HostedServiceAndBackgroundService;

public class NotifyHostedService : IHostedService
{
    private readonly ILogger<NotifyHostedService> _logger;

    public NotifyHostedService(ILogger<NotifyHostedService> logger)
    {
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var usersCount = 5;
        while (!cancellationToken.IsCancellationRequested)
        {
            for (var i = 1; i <= usersCount; i++)
            {
                _logger.LogInformation("HostedService: Notifying user {number}", i);

                await Task.Delay(5000);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("HostedService: stopping service...");

        return Task.CompletedTask;
    }
}
