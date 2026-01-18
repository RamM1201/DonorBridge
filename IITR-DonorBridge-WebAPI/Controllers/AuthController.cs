using IITR.DonorBridge.DataService.Models;
using IITR.DonorBridge.WebAPI.DataService;
using IITR.DonorBridge.WebAPI.DataService.Interfaces;
using IITR.DonorBridge.WebAPI.DataService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IITR_DonorBridge_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthRepository authRepository, IConfiguration config, ILogger<AuthController> logger)
        {
            _authRepository = authRepository;
            _config = config;
            _logger = logger;
        }
        // GET: api/<LoginController>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> GetLogin([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _authRepository.GetLoginAsync(request);
                _logger.LogInformation("Login attempt for username {Username}", request.UserID);
                if (response == null)
                {
                return Unauthorized("Invalid credentials");
                }
                response.Token = CreateJwtToken(request, response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login attempt for username {Username}", request.UserID);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        private Token CreateJwtToken(LoginRequest request,LoginResponse response)
        {
            var jwtSection = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSection["AccessTokenMinutes"]!));

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, request.UserID),
            new Claim(ClaimTypes.NameIdentifier, response.RegistrationID.ToString()),
            new Claim(ClaimTypes.Role, response.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: jwtSection["Issuer"],
                audience: jwtSection["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return new Token
            {
                AccessToken = jwt,
                Expiration = expires
            };
        }

    }
}
