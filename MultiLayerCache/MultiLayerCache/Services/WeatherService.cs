namespace MultiLayerCache.Services;

public class WeatherService(ILogger<WeatherService> logger)
{
    public async Task<WeatherForecast[]> GetForecastsAsync()
    {
        return await GetDBForecastsAsync();
    }

    private Task<WeatherForecast[]> GetDBForecastsAsync()
    {
        logger.LogInformation("Fetching from Database...");
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        return Task.FromResult(Enumerable.Range(1, 5).Select(index =>
                        new WeatherForecast
                        (
                            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                            Random.Shared.Next(-20, 55),
                            summaries[Random.Shared.Next(summaries.Length)]
                        ))
                        .ToArray());
    }
}

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

