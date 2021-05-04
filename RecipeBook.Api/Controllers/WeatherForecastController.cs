using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _configuration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger , IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("apikey")]
        public string GetAPIKey()
        {
            string apiKey = "";


            return _configuration.GetValue<string>("ApiKey");
        }

        [HttpGet("apikeytwo")] //weatherforecast/apitwo?pageSize=10&pageNumber=1
        public async Task<IActionResult> GetListAsync([FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            var printthis = "helloa";
            var ty = printthis.GetType();

            return Ok(pageSize + " " + pageNumber + " " + ty);
        }

        [HttpGet("get/{weatherId}")] //weatherforecast/get/weatherid=?
        public async Task<IActionResult> GetOnceAsync([FromRoute]Guid weatherId)
        {
            return Ok(weatherId);
            //f4f3c9ad-a99b-45d3-89f1-9c435fb70aa9
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] string number)
        {
            return Ok(number);          
        }

        [HttpDelete("{number}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int number)
        {
            return Ok(number);
        }


    }
}
