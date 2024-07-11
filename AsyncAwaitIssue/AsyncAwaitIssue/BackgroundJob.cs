namespace AsyncAwaitIssue;

public class BackgroundJob : BackgroundService
{
    private readonly ILogger<BackgroundJob> _logger;

    public BackgroundJob(ILogger<BackgroundJob> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("BackgroudJob is starting.");

        await ServiceMethodAsync(stoppingToken);

        _logger.LogInformation("BackgroudJob is stopping.");
    }

    private async Task ServiceMethodAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("ServiceMethodAsync is starting.");

        await RepositoryMethodAsync(stoppingToken);

        _logger.LogInformation("ServiceMethodAsync is stopping.");
    }

    private async Task RepositoryMethodAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("RepositoryMethodAsync is starting.");

        await DatabaseCallAsync(stoppingToken);

        _logger.LogInformation("RepositoryMethodAsync is stopping.");
    }

    private async Task DatabaseCallAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("DatabaseCallAsync is starting.");


        //await Task.Run(() => DoSomething());

        await Task.Yield();

        await Task.Delay(30_000, stoppingToken);

        _logger.LogInformation("DatabaseCallAsync is stopping.");
    }

    public void DoSomething()
    {
        _logger.LogInformation("DoSomething is starting.");

        var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));

        while (!tokenSource.Token.IsCancellationRequested)
        {
        }

        _logger.LogInformation("DoSomething is stopping.");
    }
}
