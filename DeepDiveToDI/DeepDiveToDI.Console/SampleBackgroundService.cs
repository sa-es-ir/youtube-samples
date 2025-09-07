using DeepDiveToDI.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DeepDiveToDI.Console;

public class SampleBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public SampleBackgroundService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var scopedService = scope.ServiceProvider.GetRequiredService<IScopedService>();
        var transientService = scope.ServiceProvider.GetRequiredService<ITransientService>();

        await Task.Delay(1000, stoppingToken); // Simulate some work
        System.Console.WriteLine("\n\t SampleBackgroundService executed. \n");
    }
}
