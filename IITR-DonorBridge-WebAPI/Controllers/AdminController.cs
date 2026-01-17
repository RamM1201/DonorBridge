using IITR.DonorBridge.DataService.Models;
using IITR.DonorBridge.WebAPI.DataService.Interfaces;
using IITR.DonorBridge.WebAPI.DataService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IITR_DonorBridge_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            return Ok(await _adminRepository.GetAllRegistrationsAsync());
        }

        // GET: api/<AdminController>
        [HttpGet("donations")]
        public async Task<ActionResult<IEnumerable<AdminDonationResponse>>> GetAllDonations()
        {
            return Ok(await _adminRepository.GetAllDonationsAsync());
        }

        // GET: api/<AdminController>
        [HttpGet("transactions")]
        public async Task<ActionResult<IEnumerable<AdminTransactionResponse>>> GetAllTransactions()
        {
            return Ok(await _adminRepository.GetAllTransactionsAsync());
        }
    }
}
