using Microsoft.AspNetCore.Mvc;

namespace ServerApp.Controllers;

[ApiController]
[Route("api/vejr")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("default")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Get(5);
    }
    
    [HttpGet]
    [Route("{n:int}")]
    public WeatherForecast[] Get(int n)
    {
        return Enumerable.Range(1, n).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}