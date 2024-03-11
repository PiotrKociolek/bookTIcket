using Template.Modules.Core.Application.EF;
using Template.Modules.Core.Core.Domain;
using Template.Modules.Notifications.Application.Services;
using Template.Modules.Shared.Application.Extensions;
using Template.Modules.Shared.Application.Handlers;
using Template.Modules.Shared.Core.Exceptions.Codes;
using Template.Modules.Shared.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Template.Modules.Core.Application.Messages.Commands.Users;

namespace Template.Modules.Core.Application.Handlers.Commands.Users
{
    public class SetNewPasswordHandler : BaseHandler<SetNewPassword, Unit>
    {
        private readonly TemplateContext _context;
        private readonly UserManager<Core.Domain.User> _userManager;
        private readonly INotificationService _notificationService;

        public SetNewPasswordHandler(
           TemplateContext context,
           IHttpContextAccessor httpContextAccessor,
           UserManager<Core.Domain.User> userManager,
           INotificationService notificationService)
           : base(httpContextAccessor, userManager)
        {
            _context = context;
            _notificationService = notificationService;
            _userManager = userManager;
        }

        public override async Task<Unit> Handle(SetNewPassword request, CancellationToken cancellationToken)
        {
            await base.Handle(request, cancellationToken);

            ComparePasswords(request.Password, request.RepeatPassword);

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.ChangePassword && x.Id == request.UserId.ToString())
                .OrFail("user_not_found", $"User with Id '{request.UserId}' not found");

            await ValidateResetPasswordToken(user, request.Key);

            var result = await _userManager.ResetPasswordAsync(user, request.Key, request.Password);

            if (!result.Succeeded)
            {
                throw new BusinessException("cannot_reset_password", "Cannot reset password. Contact with admin.");
            }

            user.SetChangePassword(false);

            await _context.SaveChangesAsync();

            await _notificationService.PasswordChangedAsync(user.Email);

            return Unit.Value;
        }

        private void ComparePasswords(string password, string repeatPassword)
        {
            if (!password.Equals(repeatPassword))
            {
                throw new BusinessException(ExceptionCodes.GetInvalidValueCode(repeatPassword), $"Passwords are not the same.");
            }
        }

        private async Task<bool> ValidateResetPasswordToken(Core.Domain.User user, string token)
        {
            var isValidToken = await _userManager.VerifyUserTokenAsync(
                user,
                _userManager.Options.Tokens.PasswordResetTokenProvider,
                "ResetPassword",
                token);

            if (!isValidToken)
            {
                throw new BusinessException("token_invalid", "Provided token is invalid.");
            }

            return true;
        }
    }
}
