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
            var result = await cacheManager.GetOrAddAsync("forecast-key",
                () => service.GetForecastsAsync(),
                TimeSpan.FromSeconds(5));

            return result;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();
    }
}
