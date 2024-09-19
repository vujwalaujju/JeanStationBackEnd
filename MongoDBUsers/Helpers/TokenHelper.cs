using MongoDbDemo.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace MongoDbDemo.Helpers
{
    public class TokenHelper
    {
        IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        public string GenerateToken(SignUp u)
        {
            string secret = _configuration["Jwt:secret"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim("Role", "Admin"),
                new Claim("Email", u.email),
                new Claim("phoneno",u.phonenumber),
            };
            var token = new JwtSecurityToken(
                _configuration["Jwt:issuer"],
                _configuration["Jwt:audience"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
