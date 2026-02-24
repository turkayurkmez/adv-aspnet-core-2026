using Microsoft.AspNetCore.Mvc;
using OptionsPattern.Services;

namespace OptionsPattern.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        private readonly MailService _mailService;
        private readonly DynamicMailService _dynamicMailservice;

        public WeatherForecastController(MailService mailService, DynamicMailService dynamicMailservice)
        {
            _mailService = mailService;
            _dynamicMailservice = dynamicMailservice;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _mailService.Send();
            _dynamicMailservice.Send();

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
