using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExpenseTracker.Services;
using ExpenseTracker.DTOs;
using ExpenseTracker.Models;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IConfiguration _config;

        public AuthController(AuthService service, IConfiguration config)
        {
            _authService = service;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = await _authService.Register(dto.Name, dto.Email, dto.Password);
            if (user == null) return BadRequest("Email already exists");

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _authService.Login(dto.Email, dto.Password);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = GenerateToken(user);
            return Ok(new
            {
                token,
                user = new
                {
                    id = user.Id,
                    name = user.Username,
                    email = user.Email
                }
            });
        }

        private string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("email", user.Email),
                new Claim("name", user.Username)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}