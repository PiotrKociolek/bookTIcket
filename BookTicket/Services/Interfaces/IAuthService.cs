using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BookTicket.service;

public interface IAuthService
{
    string GenerateJwtToken(string userId);
}