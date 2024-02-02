using Microsoft.AspNetCore.Mvc;
using MultiLayerCache.Services;

namespace MultiLayerCache;

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
