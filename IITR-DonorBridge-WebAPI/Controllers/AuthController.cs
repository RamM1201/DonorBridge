using IITR.DonorBridge.DataService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IITR_DonorBridge_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // GET: api/<LoginController>
        [HttpGet("{userID}")]
        public async Task<IActionResult> GetLogin(string userID, [FromBody] string password)
        {
            var login = new Login();
            return Ok(login);
        }

    }
}
