using Microsoft.Extensions.Hosting;

namespace DeepDiveToDI.Console;

public class SampleBackgroundService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(1000, stoppingToken); // Simulate some work
        System.Console.WriteLine("\n\t SampleBackgroundService executed. \n");
    }
}
