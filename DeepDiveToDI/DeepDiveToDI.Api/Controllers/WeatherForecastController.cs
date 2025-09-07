using DeepDiveToDI.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DeepDiveToDI.Api.Controllers;

[ApiController]
public class WeatherForecastController : ControllerBase
{
    //private readonly ISingletonService _singletonService;
    private readonly IScopedService _scopedService;
    private readonly ITransientService _transientService;
    private readonly IServiceScopeFactory serviceScopeFactory;
    private readonly IEmailService _emailService;
    private readonly SingletonService _singletonService;

    public WeatherForecastController(
        //ISingletonService singletonService,
        IScopedService scopedService,
        ITransientService transientService,
        IServiceScopeFactory serviceScopeFactory,
       [FromKeyedServices("Yahoo")] IEmailService emailService,
       SingletonService singletonService)
    {
        _singletonService = singletonService;
        _scopedService = scopedService;
        _transientService = transientService;
        this.serviceScopeFactory = serviceScopeFactory;
        _emailService = emailService;
    }

    [HttpGet("weather")]
    public IEnumerable<WeatherForecast> Get([FromServices] IScopedService scopedService)
    {
        _emailService.SendEmail("test@hotmail.com", "Test Subject", "Test Body");
        using var scope = serviceScopeFactory.CreateScope();
        var scopedService2 = scope.ServiceProvider.GetRequiredService<IScopedService>();

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
