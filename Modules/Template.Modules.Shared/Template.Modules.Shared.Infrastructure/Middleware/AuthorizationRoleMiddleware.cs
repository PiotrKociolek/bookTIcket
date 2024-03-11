using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Template.Modules.Core.Core.Domain;
using Template.Modules.Shared.Application.Extensions;
using Template.Modules.Shared.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Template.Modules.Shared.Infrastructure.Framework.Middleware
{
    public class AuthorizationRoleMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationRoleMiddleware(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<User> userManager)
        {
            if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                await AddRoleAsync(userManager, context);
            }
            await _next(context);
        }

        private async Task AddRoleAsync(UserManager<User> userManager, HttpContext context)
        {
            string bearerToken = context.Request.Headers["Authorization"].ToString();
            string token = bearerToken.Substring("Bearer ".Length);

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var claims = jwtToken.Claims;

            var userId = claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            if (userId == null)
            {
                throw new BusinessException("userId_not_found", "Cannot find userId claim in token");
            }

            var user = await userManager
                .FindByIdAsync(userId)
                .OrFail("user_not_found", $"User with id {userId} not found");

            var role = await userManager.GetRolesAsync(user);

            var newIdentity = new ClaimsIdentity(context.User.Identity);
            
            newIdentity.AddClaim(new Claim(ClaimTypes.Role, role[0].ToString()));

            context.User = new ClaimsPrincipal(newIdentity);
        }
    }
}