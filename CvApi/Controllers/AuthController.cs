using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CvApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var expectedUser = _config["JwtSettings:AdminUsername"];
            var expectedPass = _config["JwtSettings:AdminPassword"];

            if (request.Username != expectedUser || request.Password != expectedPass)
                return Unauthorized();

            var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Secret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer:    _config["JwtSettings:Issuer"],
                audience:  _config["JwtSettings:Audience"],
                claims:    new[] { new Claim(ClaimTypes.Name, request.Username) },
                expires:   DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }

    public record LoginRequest(string Username, string Password);
}
