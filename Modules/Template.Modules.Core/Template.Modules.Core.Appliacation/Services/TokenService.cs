using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Template.Modules.Core.Core.Domain;

namespace Template.Modules.Core.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(string email, string id)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim("UserId", id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Secret").Value!));
            var issuer = _configuration.GetSection("JWT:ValidIssuer").Value;
            var audience = _configuration.GetSection("JWT:ValidAudience").Value;
            int expires = int.Parse(_configuration.GetSection("JWT:TokenValidityInMinutes").Value);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(expires),
                    signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("UserId", user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Secret").Value!));
            int expires = int.Parse(_configuration.GetSection("JWT:RefreshTokenValidityInDays").Value);
            DateTime tokenExpiresIn = DateTime.Now.AddDays(expires);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: tokenExpiresIn,
                    signingCredentials: creds
                );

            var refreshToken = new RefreshToken(
                Guid.NewGuid(),
                new JwtSecurityTokenHandler().WriteToken(token),
                user,
                tokenExpiresIn
                );

            return refreshToken;
        }
    }
}
