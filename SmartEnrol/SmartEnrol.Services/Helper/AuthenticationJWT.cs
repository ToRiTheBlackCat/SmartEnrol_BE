using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartEnrol.Repositories.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnrol.Services.Helper
{
    public class AuthenticationJWT
    {
        private readonly IConfiguration _configuration;
        public AuthenticationJWT(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateJwtToken(Account? account)
        {
            if (account == null)
                return "";

            var jwtKey = _configuration["JWT:Secret"];
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey ?? ""));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString().Trim()),
                new Claim(ClaimTypes.Role, account.Role.RoleName.Trim()),
                new Claim(ClaimTypes.Name, account.AccountName.Trim())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: signingCredentials
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return accessToken;
        }
    }
}
