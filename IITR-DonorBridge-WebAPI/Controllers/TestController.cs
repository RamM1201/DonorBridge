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

        //// GET api/<TestController>/5
        //[HttpGet("{id}")]
        //public string GetById(int id)
        //{
        //    return "value";
        //}

        //// POST api/<TestController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<TestController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<TestController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
