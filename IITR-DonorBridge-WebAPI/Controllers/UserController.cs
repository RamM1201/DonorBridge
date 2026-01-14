using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IITR_DonorBridge_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        

        // GET api/<UserController>/5
        [HttpGet("registration/{id}")]
        public string GetUserRegistrationByID(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost("registration",Name ="UserRegistration")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("registration/{id}")]
        public void UpdateUserRegistration(int id, [FromBody] string value)
        {
        }

        
    }
}
