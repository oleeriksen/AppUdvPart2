using Microsoft.AspNetCore.Mvc;
using Core;

namespace ServerApp.Controllers;

[ApiController]
[Route("api/vejr")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

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
        List<WeatherForecast> result = new();
        for (int index = 1; index <= n; index++) {
            var forecast = new WeatherForecast {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
            result.Add(forecast);
        }
        return result.ToArray();
    }
    
    [HttpGet]
    [Route("ping")]
    public string Ping()
    {
        return "Weather API - version 0.1";
    }
}