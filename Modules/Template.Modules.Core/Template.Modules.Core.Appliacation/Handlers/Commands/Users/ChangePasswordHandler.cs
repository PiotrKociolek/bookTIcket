using Template.Modules.Core.Application.Messages.Commands.Users;
using Template.Modules.Core.Core.Domain;
using Template.Modules.Shared.Application.Handlers;
using Template.Modules.Shared.Core.Exceptions;
using Template.Modules.Shared.Core.Exceptions.Codes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Template.Modules.Core.Application.Handlers.Commands.Users
{
    public class ChangePasswordHandler : BaseHandler<ChangePassword, Unit>
    {
        private readonly UserManager<User> _userManager;

        public ChangePasswordHandler(
           IHttpContextAccessor httpContextAccessor,
           UserManager<User> userManager)
           : base(httpContextAccessor, userManager)
        {
            _userManager = userManager;
        }

        public override async Task<Unit> Handle(ChangePassword request, CancellationToken cancellationToken)
        {
            await base.Handle(request, cancellationToken);

            ComparePasswords(request.NewPassword, request.NewPasswordRepeat);

            var user = await GetCurrentUserAsync();
            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            if (!result.Succeeded)
            {
                throw new BusinessException(result.Errors.First().Code, result.Errors.First().Description);
            }

            return Unit.Value;
        }

        private void ComparePasswords(string password, string repeatPassword)
        {
            if (!password.Equals(repeatPassword))
            {
                throw new BusinessException(ExceptionCodes.GetInvalidValueCode(repeatPassword), $"Passwords are not the same.");
            }
        }
    }
}
