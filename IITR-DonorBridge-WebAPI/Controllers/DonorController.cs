using IITR.DonorBridge.WebAPI.DataService.Interfaces;
using IITR.DonorBridge.WebAPI.DataService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Razorpay.Api;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IITR_DonorBridge_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "donor")]
    public class DonorController : ControllerBase
    {
        private readonly IDonorRepository _donorRepository;
        private readonly ILogger<DonorController> _logger;
        public DonorController(IDonorRepository donorRepository, ILogger<DonorController> logger)
        {
            _donorRepository = donorRepository;
            _logger = logger;
        }
        private string CalculateHash(string payload, string secret)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(secret);
            byte[] payloadBytes = Encoding.UTF8.GetBytes(payload);

            using (var hmac = new HMACSHA256(keyBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(payloadBytes);

                // Convert the bytes to a lowercase hex string
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
        // GET: api/<DonorController>
        [HttpGet("donations/{registrationId}")]
        public async Task<ActionResult<IEnumerable<DonorDonationResponse>>> GetAllDonations(int registrationId)
        {
            try
            {
                _logger.LogInformation("Donor with registrationId {RegistrationId} requested all donations", registrationId);
                return Ok(await _donorRepository.GetAllDonationsAsync(registrationId)); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while donor with registrationId {RegistrationId} fetching all donations", registrationId);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // GET api/<DonorController>/5
        [HttpGet("transactions/{donationId}")]
        public async Task<ActionResult<IEnumerable<DonorTransactionResponse>>> GetTransactionsById(int donationId)
        {
            try { 
                _logger.LogInformation("Donor requested transactions for donationId {DonationId}", donationId);
                return Ok(await _donorRepository.GetTransactionsByDonationIdAsync(donationId)); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while donor fetching transactions for donationId {DonationId}", donationId);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // POST api/<DonorController>
        [HttpPost("donations")]
        public async Task<IActionResult> CreateDonation([FromBody] DonorDonationRequest request)
        {
            try
            {
                _logger.LogInformation("Donor with registrationId {RegistrationId} is creating a donation", request.UserRegistrationId);
                var donationId = await _donorRepository.CreateDonationAsync(request);
                RazorpayClient client = new RazorpayClient("rzp_test_S4vqgKplB3Kh5m", "hTCHeig0h1mZPiqoN1qbvg2A");
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", request.Amount * 100); // amount in the smallest currency unit
                options.Add("currency", "INR");
                options.Add("receipt", "order_rcptid_" + donationId);
                Order order = client.Order.Create(options);
                string orderId = order["id"].ToString();
                DonorTransactionRequest transactionRequest = new DonorTransactionRequest
                {
                    DonationId = donationId,
                    OrderId = orderId
                };
                var transactionId = await _donorRepository.CreateTransactionAsync(transactionRequest);
                return StatusCode(201, new { DonationId = donationId, OrderId = orderId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while donor with registrationId {RegistrationId} creating a donation", request.UserRegistrationId);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // POST api/<DonorController>/5
        [HttpPost("transactions/{donationId}")]
        public async Task<IActionResult> CreateTransaction(int donationId)
        {
            try
            {
                _logger.LogInformation("Donor is creating a transaction for donationId {DonationId}", donationId);
                RazorpayClient client = new RazorpayClient("rzp_test_S4vqgKplB3Kh5m", "hTCHeig0h1mZPiqoN1qbvg2A");
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", await _donorRepository.GetAmountForDonation(donationId) * 100); // amount in the smallest currency unit
                options.Add("currency", "INR");
                options.Add("receipt", "order_rcptid_" + donationId);
                Order order = client.Order.Create(options);
                string orderId = order["id"].ToString();
                DonorTransactionRequest transactionRequest = new DonorTransactionRequest
                {
                    DonationId = donationId,
                    OrderId = orderId
                };
                var transactionId = await _donorRepository.CreateTransactionAsync(transactionRequest);
                return StatusCode(201, new { DonationId = donationId, OrderId = orderId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while donor creating a transaction for donationId {DonationId}", donationId);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost("verify")]
        public async Task<ActionResult<DonorTransactionResponse>> VerifyPayment([FromBody] PaymentVerificationRequest request)
        {
            try
            {
                _logger.LogInformation("Verifying payment for OrderId {OrderId}", request.RazorpayOrderId);
                // 1. Combine OrderID and PaymentID with a '|'
                string payload = request.RazorpayOrderId + "|" + request.RazorpayPaymentId;

                // 2. Secret Key from your Gateway Dashboard
                string secret = "hTCHeig0h1mZPiqoN1qbvg2A";

                // 3. Generate HMAC-SHA256 Hash
                string generatedSignature = CalculateHash(payload, secret);

                // 4. THE CRITICAL CHECK
                if (generatedSignature == request.RazorpaySignature)
                {
                    TransactionStatusUpdateRequest transactionStatusUpdateRequest = new TransactionStatusUpdateRequest
                    {
                        OrderId = request.RazorpayOrderId,
                        Status = "completed",
                        PaymentId = request.RazorpayPaymentId
                    };
                    var response = await _donorRepository.UpdateDonationStatus(transactionStatusUpdateRequest);
                    return StatusCode(201, response);
                }
                else
                {
                    TransactionStatusUpdateRequest transactionStatusUpdateRequest = new TransactionStatusUpdateRequest
                    {
                        OrderId = request.RazorpayOrderId,
                        Status = "failed",
                        PaymentId = null
                    };
                    var respons = await _donorRepository.UpdateDonationStatus(transactionStatusUpdateRequest);
                    return BadRequest("Invalid Signature. Payment could not be verified.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while verifying payment for OrderId {OrderId}", request.RazorpayOrderId);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
