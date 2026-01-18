using IITR.DonorBridge.DataService.Models;
using IITR.DonorBridge.WebAPI.DataService.Interfaces;
using IITR.DonorBridge.WebAPI.DataService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IITR_DonorBridge_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        // GET api/<UserController>/5
        [HttpGet("registration/{id}")]
        public async Task<ActionResult<RegistrationResponse>> GetUserRegistrationByID(int id)
        {
            try {
                var response = await _userRepository.GetUserRegistrationByIdAsync(id);
                _logger.LogInformation("Fetched user registration for RegistrationID {RegistrationID}", id);
                if (response == null)
                {
                    return NotFound($"Registration has not initiated");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching user registration for RegistrationID {RegistrationID}", id);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // POST api/<UserController>
        [HttpPost("registration",Name ="UserRegistration")]
        public async Task<ActionResult<LoginResponse>> CreateUserRegistration([FromBody] RegistrationRequest request)
        {
            try 
            {
                var response = await _userRepository.CreateUserRegistrationAsync(request);
                _logger.LogInformation("Created user registration for UserID {UserID}", request.UserId);
                if (response == null)
                {
                    return BadRequest("Registration failed");
                }
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating user registration for UserID {UserID}", request.UserId);
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
