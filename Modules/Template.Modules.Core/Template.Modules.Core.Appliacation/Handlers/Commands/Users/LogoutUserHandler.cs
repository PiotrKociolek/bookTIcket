using Template.Modules.Core.Application.EF;
using Template.Modules.Core.Application.Messages.Commands.Users;
using Template.Modules.Shared.Application.Extensions;
using Template.Modules.Shared.Application.Handlers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Template.Modules.Core.Application.Handlers.Commands.Users
{
    public class LogoutUserHandler : BaseHandler<LogoutUser, Unit>
    {
        private readonly TemplateContext _context;
        private readonly HttpContext _httpContext;

        public LogoutUserHandler(
            UserManager<Core.Domain.User> userManager,
            IHttpContextAccessor httpContextAccessor,
            TemplateContext context)
           : base(httpContextAccessor, userManager)
        {
            _context = context;
            _httpContext = GetHttpContext();
        }

        public override async Task<Unit> Handle(LogoutUser request, CancellationToken cancellationToken)
        {
            await base.Handle(request, cancellationToken);

            var user = await _context.Users
                .Include(x => x.Tokens)
                .FirstOrDefaultAsync(x => x.Id == request.Id.ToString() && x.IsDeleted == false)
                .OrFail("user_not_found", $"User with Id '{request.Id}' not found.");

            if (_httpContext.Request.Cookies["refreshToken"] != null)
            {
                var refreshToken = _httpContext.Request.Cookies["refreshToken"];

                var tokenToDelete = user.Tokens
                    .FirstOrDefault(x => x.Token == refreshToken)
                    .OrFail("token_not_found", $"Token '{refreshToken}' not found.");

                user.RemoveRefreshToken(tokenToDelete);

                await _context.SaveChangesAsync();

                _httpContext.Response.Cookies.Delete("refreshToken");
            }

            return Unit.Value;
        }

    }
}
