using Template.Modules.Core.Application.Dto.Accounts;
using Template.Modules.Core.Application.EF;
using Template.Modules.Core.Application.Services;
using Template.Modules.Shared.Application.Extensions;
using Template.Modules.Shared.Application.Handlers;
using Template.Modules.Shared.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Template.Modules.Core.Application.Handlers.Commands.Users
{
    public class RefreshTokenHandler : BaseHandler<Messages.Commands.Users.RefreshToken, TokensDto>
    {
        private readonly HttpContext _httpContext;
        private readonly TemplateContext _context;
        private readonly ITokenService _tokenService;

        public RefreshTokenHandler(
            IHttpContextAccessor httpContextAccessor,
            UserManager<Core.Domain.User> userManager,
            IConfiguration configuration,
            TemplateContext context,
            ITokenService tokenService)
           : base(httpContextAccessor, userManager)
        {
            _httpContext = GetHttpContext();
            _context = context;
            _tokenService = tokenService;
        }

        public override async Task<TokensDto> Handle(Messages.Commands.Users.RefreshToken request, CancellationToken cancellationToken)
        {
            await base.Handle(request, cancellationToken);

            var refreshToken = _httpContext.Request.Cookies["refreshToken"];

            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                throw new BusinessException("auth_failed", "Refresh token not provided.");
            }

            await IsRefreshTokenExpiredAsync(refreshToken);
            var userId = GetUserIdFromRefreshToken(refreshToken);

            var user = await _context.Users
                .Include(x => x.Tokens)
                .FirstOrDefaultAsync(x => x.Id == userId.ToString())
                .OrFail("user_not_found", $"User with id '{userId}' not found");

            if (!user.Tokens.Any(x => x.Token == refreshToken))
            {
                throw new BusinessException("token_not_found", $"Cannot find find token '{refreshToken}' assigned to this user.");
            }

            var oldRefreshToken = user.Tokens
                .FirstOrDefault(x => x.Token == refreshToken);

            await RemoveRefreshTokenAsync(user, oldRefreshToken);
            var newRefreshToken = await CreateRefreshTokenAsync(user);
            return PrepareResponse(newRefreshToken, user);
        }

        private async Task<Unit> RemoveRefreshTokenAsync(Core.Domain.User user, Core.Domain.RefreshToken oldRefreshToken)
        {
            user.RemoveRefreshToken(oldRefreshToken);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }

        private async Task<Core.Domain.RefreshToken> CreateRefreshTokenAsync(Core.Domain.User user)
        {
            var newRefreshToken = _tokenService.GenerateRefreshToken(user);
            user.AddRefreshToken(newRefreshToken);
            await _context.SaveChangesAsync();

            var cookieOptions = new CookieOptions
            {
                Expires = newRefreshToken.ExpiresIn,
                SameSite = SameSiteMode.None
            };
            _httpContext.Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            return newRefreshToken;
        }

        private TokensDto PrepareResponse(Core.Domain.RefreshToken newRefreshToken, Core.Domain.User user)
        {
            string accessToken = _tokenService.GenerateJwtToken(user.Email, user.Id);
            var token = new TokensDto()
            {
                AccessToken = accessToken
            };

            return token;
        }

        private async Task<Unit> IsRefreshTokenExpiredAsync(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
            {
                throw new BusinessException("invalid_jwt_token", $"Token '{token}' is invalid.");
            }

            var expirationTime = jwtToken.ValidTo;
            var now = DateTime.Now;

            if (now > expirationTime)
            {
                var userToken = await _context.RefreshTokens
                    .FirstOrDefaultAsync(x => x.Token == token)
                    .OrFail("token_not_found", $"Token '{token}' not found.");

                _context.Remove(userToken);

                await _context.SaveChangesAsync();

                throw new UnauthorizedAccessException("Refresh token has expired.");
            }

            return Unit.Value;
        }

        private Guid GetUserIdFromRefreshToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var claims = jwtToken.Claims;

            var userId = claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            if (userId == null)
            {
                throw new BusinessException("userId_not_found", "Cannot find userId claim in token");
            }

            return Guid.Parse(userId);
        }
    }
}
