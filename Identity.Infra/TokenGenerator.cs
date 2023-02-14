using Identity.Application.InvertedDependencies;
using Identity.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Infra
{
    internal class TokenGenerator : ITokenGenerator
    {
        public string BuildToken(string secret, IdentityUser userData,  Claim[] claims)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            var writtenToken = jwtTokenHandler.WriteToken(token);

            return writtenToken;
        }
    }
}
