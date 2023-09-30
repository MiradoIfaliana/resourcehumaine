using Microsoft.AspNetCore.Mvc;
using rhwebapi.Models;
//namespace rhwebapi.Controllers;
namespace servicewebapi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    //[ApiController] : Cette annotation indique que la classe WeatherForecastController est un contrôleur API. Elle applique automatiquement certaines conventions de routage et de comportement pour les actions de contrôleur.
    //[Route("[controller]")] : Cette annotation définit un préfixe de routage pour les actions de ce contrôleur. Dans ce cas, [controller] sera remplacé par le nom du contrôleur, ce qui signifie que l'URL de base pour les actions de ce contrôleur sera /WeatherForecast.
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {   
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
    [HttpGet("cities", Name = "GetCities")]
    public IEnumerable<string> GetCities(string popota)
    {
        string[] cities = new string[]
        {
           popota, "Los Angeles", "Chicago", "Miami", "San Francisco"
        };
        return cities;
    }

    [HttpGet("saveannonce", Name = "GetSaveannonce")]
    public IEnumerable<string> GetSAve(string datas)
    {
        //instruction de sauvegarde de l'annonce
        return null;
    }
}
