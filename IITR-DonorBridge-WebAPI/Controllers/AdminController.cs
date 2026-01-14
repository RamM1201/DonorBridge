using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IITR_DonorBridge_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        // GET: api/<AdminController>
        [HttpGet("users")]
        public IEnumerable<string> GetAllUsers()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/<AdminController>
        [HttpGet("donations")]
        public IEnumerable<string> GetAllDonations()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/<AdminController>
        [HttpGet("transactions")]
        public IEnumerable<string> GetAllTransactions()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
