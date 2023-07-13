using Microsoft.AspNetCore.Mvc;
using ModelBinder.Binders;

namespace ModelBinder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("")]
        public IEnumerable<string> Get([ModelBinder(typeof(ValueToListBinder<int>))] List<int> ids)
        {
            return ids.Select(i => Summaries[i]);
        }

        [HttpGet("valid/{id}")]
        public string Get(int id, [ModelBinder(typeof(OptionsBinder))] bool isValid)
        {
            return $"{id} is {(isValid ? "valid" : "notValid")}";
        }
    }
}