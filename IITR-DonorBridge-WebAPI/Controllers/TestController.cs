using Microsoft.AspNetCore.Mvc;
using IITR.DonorBridge.DataService.Interfaces;
using IITR.DonorBridge.DataService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IITR_DonorBridge_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestRepository _Repository;
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];
        public TestController(ITestRepository repository)
        {
            _Repository = repository;
        }
        // GET: api/<TestController>
        [HttpGet("all",Name ="GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _Repository.GetAllUsersAsync();
            return Ok(users);
        }
        

        [HttpGet("rawdata")]
        public IEnumerable<WeatherForecast> GetWithoutSql()
        {
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
