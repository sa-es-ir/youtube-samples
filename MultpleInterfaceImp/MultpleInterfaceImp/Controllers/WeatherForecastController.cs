using Microsoft.AspNetCore.Mvc;

namespace MultpleInterfaceImp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly StrategyFooService strategyFooService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            StrategyFooService strategyFooService)
        {
            _logger = logger;
            this.strategyFooService = strategyFooService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var fooService = strategyFooService.Invoke(FooType.Foo);

            fooService.DoSomething();

            var barService = strategyFooService.Invoke(FooType.Bar);

            barService.DoSomething();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}