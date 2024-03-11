using Template.Modules.Core.Application.EF;
using Template.Modules.Core.Application.Messages.Commands.Users;
using Template.Modules.Core.Core.Domain;
using Template.Modules.Notifications.Application.Services;
using Template.Modules.Shared.Application.Extensions;
using Template.Modules.Shared.Application.Handlers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Web;

namespace Template.Modules.Core.Application.Handlers.Commands.Users
{
    public class ResetPasswordHandler : BaseHandler<ResetPassword, Unit>
    {
        private readonly TemplateContext _context;
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Core.Domain.User> _userManager;

        public ResetPasswordHandler(
           TemplateContext context,
           IHttpContextAccessor httpContextAccessor,
           UserManager<Core.Domain.User> userManager,
           INotificationService notificationService,
           IConfiguration configuration)
           : base(httpContextAccessor, userManager)
        {
            _context = context;
            _notificationService = notificationService;
            _configuration = configuration;
            _userManager = userManager;
        }

        public override async Task<Unit> Handle(ResetPassword request, CancellationToken cancellationToken)
        {
            await base.Handle(request, cancellationToken);

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email)
                .OrFail("user_not_found", $"User with Email '{request.Email}' not found");

            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            resetToken = HttpUtility.UrlEncode(resetToken);

            user.SetChangePassword(true);

            var resetPasswordPath = _configuration.GetSection("PasswordReset:ResetPasswordPath").Value;

            await _notificationService.ResetPasswordAsync(user.Email, $"{resetPasswordPath}?token={resetToken}&userId={user.Id}");

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
