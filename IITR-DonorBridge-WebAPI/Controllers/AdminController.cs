using IITR.DonorBridge.DataService.Models;
using IITR.DonorBridge.WebAPI.DataService.Interfaces;
using IITR.DonorBridge.WebAPI.DataService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IITR_DonorBridge_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private ILogger<AdminController> _logger;
        public AdminController(IAdminRepository adminRepository, ILogger<AdminController> logger)
        {
            _adminRepository = adminRepository;
            _logger = logger;
        }
        // GET: api/<AdminController>
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<RegistrationResponse>>> GetAllUsers()
        {
            try
            { 
                _logger.LogInformation("Admin requested all user registrations");
                return Ok(await _adminRepository.GetAllRegistrationsAsync()); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while admin fetching all user registrations");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // GET: api/<AdminController>
        [HttpGet("donations")]
        public async Task<ActionResult<IEnumerable<AdminDonationResponse>>> GetAllDonations()
        {
           try
            { 
                _logger.LogInformation("Admin requested all donations");
                return Ok(await _adminRepository.GetAllDonationsAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while admin fetching all donations");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // GET: api/<AdminController>
        [HttpGet("transactions")]
        public async Task<ActionResult<IEnumerable<AdminTransactionResponse>>> GetAllTransactions()
        {
           try
            { 
                _logger.LogInformation("Admin requested all transactions");
                return Ok(await _adminRepository.GetAllTransactionsAsync()); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while admin fetching all transactions");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
