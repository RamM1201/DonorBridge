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
    [Authorize(Roles = "admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        // GET: api/<AdminController>
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<RegistrationResponse>>> GetAllUsers()
        {
            try
            { return Ok(await _adminRepository.GetAllRegistrationsAsync()); }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // GET: api/<AdminController>
        [HttpGet("donations")]
        public async Task<ActionResult<IEnumerable<AdminDonationResponse>>> GetAllDonations()
        {
           try
            { return Ok(await _adminRepository.GetAllDonationsAsync()); }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // GET: api/<AdminController>
        [HttpGet("transactions")]
        public async Task<ActionResult<IEnumerable<AdminTransactionResponse>>> GetAllTransactions()
        {
           try
            { return Ok(await _adminRepository.GetAllTransactionsAsync()); }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
