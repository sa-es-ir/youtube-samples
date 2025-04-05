using Microsoft.AspNetCore.Mvc;

namespace DeepDiveToDI.Api.Controllers;

[ApiController]
public class WeatherForecastController : ControllerBase
{
    [HttpGet("weather")]
    public IEnumerable<WeatherForecast> Get()
    {
        string[] Summaries =
            [
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            ];

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
