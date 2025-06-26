using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class LoginService : ILoginService
{
    
        private readonly IConfiguration _configuration;
        private readonly IUserMgmtService _userMgmtService;

        public LoginService(IConfiguration configuration, IUserMgmtService userMgmtService)
        {
            _configuration = configuration;
            _userMgmtService = userMgmtService;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userMgmtService.ValidateUserCredentialsAsync(email, password);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            return GenerateJwtToken(user);
        }
        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"] ?? throw new InvalidOperationException()));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"] ?? "60" )),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
}