using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IITR_DonorBridge_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        // GET: api/<DonorController>
        [HttpGet("{id}")]
        public IEnumerable<string> GetAllDonations(int id)
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DonorController>/5
        [HttpGet("transactions/{id}")]
        public string GetTransactionsById(int id)
        {
            return "value";
        }

        // POST api/<DonorController>
        [HttpPost]
        public void CreateDonation([FromBody] string value)
        {
        }

        // POST api/<DonorController>/5
        [HttpPost("{id}")]
        public void CreateTransaction(int id, [FromBody] string value)
        {
        }
    }
}
