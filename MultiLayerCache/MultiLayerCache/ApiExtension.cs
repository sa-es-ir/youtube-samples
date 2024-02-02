using Microsoft.AspNetCore.Mvc;
using MultiLayerCache.CacheProviders;
using MultiLayerCache.Services;

namespace MultiLayerCache;

public static class ApiExtension
{
    public static void MapWeatherApi(this WebApplication app)
    {
        app.MapGet("/weatherforecast", async ([FromServices] WeatherService service,
            [FromServices] CacheManager cacheManager) =>
        {
            WeatherForecast[] forecast = await cacheManager
            .GetOrAddAsync("w-c-f",
            () => service.GetForecastsAsync(),
            TimeSpan.FromSeconds(10));

            return forecast;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();
    }
}
