using Microsoft.AspNetCore.Mvc;
using MutliLayerCache.Services;

namespace MutliLayerCache;

public static class ApiExtension
{
    public static void MapWeatherApi(this WebApplication app)
    {
        app.MapGet("/weatherforecast", async ([FromServices] WeatherService service) =>
        {
            WeatherForecast[] forecast = await service.GetForecastsAsync();
            return forecast;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();
    }
}
