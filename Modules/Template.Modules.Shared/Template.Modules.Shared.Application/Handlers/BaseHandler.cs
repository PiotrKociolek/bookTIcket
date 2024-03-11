using Template.Modules.Core.Core.Domain;
using Template.Modules.Shared.Application.Extensions;
using Template.Modules.Shared.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Template.Modules.Shared.Application.Handlers
{
    public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly HttpContext HttpContext;
        private readonly UserManager<User> _userManager;

        protected BaseHandler(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
             HttpContext = GetHttpContext();
            _userManager = userManager;
        }
        
        public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            return await Task.FromResult<TResponse>(default);
        }

        protected HttpContext GetHttpContext()
        {
            if (_httpContextAccessor.HttpContext is not null)
            {
                return _httpContextAccessor.HttpContext;    
            }
            
            throw new Exception("HttpContext is null");
        }

        protected async Task<User> GetCurrentUserAsync()
        {
            string bearerToken = HttpContext.Request.Headers["Authorization"].ToString();
            string token = bearerToken.Substring("Bearer ".Length);

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var claims = jwtToken.Claims;

            var userId = claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            if (userId == null) 
            {
                throw new BusinessException("userId_not_found", "Cannot find userId claim in token");
            }

            var user = await _userManager
                .FindByIdAsync(userId)
                .OrFail("user_not_found", $"User with id '{userId}' not found");

            return user;
        }
    }
}