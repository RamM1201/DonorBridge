using IITR.DonorBridge.WebAPI.DataService.Interfaces;
using IITR.DonorBridge.WebAPI.DataService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Razorpay.Api;
using System.Security.Cryptography;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IITR_DonorBridge_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly IDonorRepository _donorRepository;
        public DonorController(IDonorRepository donorRepository)
        {
            _donorRepository = donorRepository;
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
            return Ok(await _donorRepository.GetAllDonationsAsync(registrationId));
        }

        // GET api/<DonorController>/5
        [HttpGet("transactions/{donationId}")]
        public async Task<ActionResult<IEnumerable<DonorTransactionResponse>>> GetTransactionsById(int donationId)
        {
            return Ok(await _donorRepository.GetTransactionsByDonationIdAsync(donationId));
        }

        // POST api/<DonorController>
        [HttpPost("donations")]
        public async Task<IActionResult> CreateDonation([FromBody] DonorDonationRequest request)
        {
            var donationId= await _donorRepository.CreateDonationAsync(request);
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

        // POST api/<DonorController>/5
        [HttpPost("transactions/{donationId}")]
        public async Task<IActionResult> CreateTransaction(int donationId)
        {
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

        [HttpPost("verify")]
        public async Task<ActionResult<DonorTransactionResponse>> VerifyPayment([FromBody] PaymentVerificationRequest request)
        {
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
                return StatusCode(201,response);
            }
            else
            {
                TransactionStatusUpdateRequest transactionStatusUpdateRequest = new TransactionStatusUpdateRequest
                {
                    OrderId = request.RazorpayOrderId,
                    Status = "failed",
                    PaymentId = null
                };
                var respons=await _donorRepository.UpdateDonationStatus(transactionStatusUpdateRequest);
                return BadRequest("Invalid Signature. Payment could not be verified.");
            }
        }
    }
}
