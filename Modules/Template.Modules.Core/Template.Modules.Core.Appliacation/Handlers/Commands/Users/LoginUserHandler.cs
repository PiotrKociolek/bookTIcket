using Template.Modules.Core.Application.Dto.Users;
using Template.Modules.Core.Application.EF;
using Template.Modules.Core.Application.Messages.Commands.Users;
using Template.Modules.Core.Application.Services;
using Template.Modules.Shared.Application.Handlers;
using Template.Modules.Shared.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Template.Modules.Core.Application.Handlers.Commands.Users
{
    public class LoginUserHandler : BaseHandler<LoginUser, LoggedUserDto>
    {
        private readonly SignInManager<Core.Domain.User> _signInManager;
        private readonly TemplateContext _context;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly UserManager<Core.Domain.User> _userManager;
        private readonly ITokenService _tokenService;
        public LoginUserHandler(
            IHttpContextAccessor httpContextAccessor,
            UserManager<Core.Domain.User> userManager,
            SignInManager<Core.Domain.User> signInManager,
            IConfiguration configuration,
            TemplateContext context,
            ITokenService tokenService)
           : base(httpContextAccessor, userManager)
        {
            _signInManager = signInManager;
            _context = context;
            _HttpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public override async Task<LoggedUserDto> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            await base.Handle(request, cancellationToken);

            var user = await _userManager
                .FindByEmailAsync(request.Email);

            if (user == null || user.IsDeleted)
            {
                throw new BusinessException("user_not_found", $"User with email '{request.Email}' not found or is deleted.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                string token = _tokenService.GenerateJwtToken(request.Email, user.Id);
                var refreshToken = _tokenService.GenerateRefreshToken(user);

                user.AddRefreshToken(refreshToken);

                await _context.SaveChangesAsync(cancellationToken);

                var loggedUser = new LoggedUserDto();
                loggedUser.Email = user.Email;
                loggedUser.Id = Guid.Parse(user.Id);
                loggedUser.AccessToken = token;
                var cookieOptions = new CookieOptions
                { 
                    Expires = refreshToken.ExpiresIn
                };

                _HttpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

                return loggedUser;
            }

            throw new BusinessException("auth_failed", "Invalid credentials.");
        }
    }
}
