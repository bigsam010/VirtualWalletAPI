using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VirtualWalletApi.Data.Entities;
using static VirtualWalletApi.Installer.ServiceInstaller;

namespace VirtualWalletApi.Utilities
{
    public class JWTHelper
    {
        public static string GetJWTToken(Customer customer)
        {
            var claims = new List<Claim>()
           {
               new Claim(JwtRegisteredClaimNames.Sub, customer.Email),
               new Claim("Id", customer.Id.ToString()),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, customer.Email),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
               new Claim(ClaimTypes.Email, customer.Email),

           };
            var tokenConfig = new TokenConfiguration();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(tokenConfig.AccessExpiration));

            var token = new JwtSecurityToken(tokenConfig.Issuer, tokenConfig.Audience, claims, expires: expires,
                notBefore: DateTime.Now, signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public class TokenReturnHelper
        {
            public string Token { get; set; }
            public DateTime ExpiresIn { get; set; }

        }
    }
}
