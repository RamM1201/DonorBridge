using IITR.DonorBridge.DataService.Models;
using IITR.DonorBridge.WebAPI.DataService.Interfaces;
using IITR.DonorBridge.WebAPI.DataService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IITR_DonorBridge_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        // GET api/<UserController>/5
        [HttpGet("registration/{id}")]
        public async Task<ActionResult<RegistrationResponse>> GetUserRegistrationByID(int id)
        {
            try {var response = await _userRepository.GetUserRegistrationByIdAsync(id);
            if (response == null)
            {
                return NotFound($"Registration has not initiated");
            }
            return Ok(response);}
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // POST api/<UserController>
        [HttpPost("registration",Name ="UserRegistration")]
        public async Task<ActionResult<LoginResponse>> CreateUserRegistration([FromBody] RegistrationRequest request)
        {
            try {var response = await _userRepository.CreateUserRegistrationAsync(request);
            if (response == null)
            {
                return BadRequest("Registration failed");
            }
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("registration/{id}")]
        public void UpdateUserRegistration(int id, [FromBody] string value)
        {
        }

        
    }
}
