using Template.Modules.Core.Application.Dto.Accounts;
using Template.Modules.Core.Application.Messages.Queries.Users;
using Template.Modules.Shared.Application.Extensions;
using Template.Modules.Shared.Application.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Web;
using Template.Modules.Core.Core.Domain;

namespace Template.Modules.Core.Application.Handlers.Queries.Users
{
    public class ValidateResetPasswordTokenHandler : BaseHandler<ValidateResetPasswordToken, ValidateResetPasswordTokenDto>
    {
        private readonly UserManager<User> _userManager;

        public ValidateResetPasswordTokenHandler(
           IHttpContextAccessor httpContextAccessor,
           UserManager<User> userManager)
           : base(httpContextAccessor, userManager)
        {
            _userManager = userManager;
        }

        public override async Task<ValidateResetPasswordTokenDto> Handle(ValidateResetPasswordToken request, CancellationToken cancellationToken)
        {
            await base.Handle(request, cancellationToken);
            var user = await _userManager.FindByIdAsync(request.UserId)
                .OrFail("user_not_found", $"User with id '{request.UserId}' not found.");

            var isValidToken = await _userManager.VerifyUserTokenAsync(
                user,
                _userManager.Options.Tokens.PasswordResetTokenProvider,
                "ResetPassword",
                request.Token);

            var response = new ValidateResetPasswordTokenDto()
            {
                IsTokenValid = isValidToken
            };

            return response;
        }
    }
}
