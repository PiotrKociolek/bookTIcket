namespace BookTicket.utilities;

using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

public class JwtAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly SymmetricSecurityKey _securityKey;

    public JwtAuthenticationMiddleware(RequestDelegate next, string secretKey)
    {
        _next = next;
        _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
    }

    public async Task Invoke(HttpContext context)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _securityKey,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(x => x.Type == "userId").Value;

            context.Items["UserId"] = userId;
        }
        catch (Exception)
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("Invalid token.");
            return;
        }

        await _next(context);
    }
}