using Template.Modules.Core.Application.EF;
using Template.Modules.Core.Application.Messages.Commands.Users;
using Template.Modules.Core.Core.Domain;
using Template.Modules.Notifications.Application.Services;
using Template.Modules.Shared.Application.Extensions;
using Template.Modules.Shared.Application.Handlers;
using Template.Modules.Shared.Core.Exceptions;
using Template.Modules.Shared.Core.Exceptions.Codes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Web;

namespace Template.Modules.Core.Application.Handlers.Commands.Users
{
    public class CreateUserHandler : BaseHandler<CreateUser, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _configuration;

        public CreateUserHandler(
           IHttpContextAccessor httpContextAccessor,
           UserManager<User> userManager,
           RoleManager<IdentityRole> roleManager,
           INotificationService notificationService,
           IConfiguration configuration)
           : base(httpContextAccessor, userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _notificationService = notificationService;
            _configuration = configuration;
        }

        public override async Task<Unit> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            await base.Handle(request, cancellationToken);

            await CheckIfExistAsync(request.Email);

            if (!await _roleManager.RoleExistsAsync(request.Role))
            {
                throw new BusinessException("role_not_found", $"Role '{request.Role}' not found");
            }

            var user = new User(request.Id, request.Email, request.UserName, true);
            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                throw new BusinessException(result.Errors.First().Code, result.Errors.First().Description);
            }

            string setPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            setPasswordToken = HttpUtility.UrlEncode(setPasswordToken);
            var setPasswordPath = _configuration.GetSection("PasswordReset:ResetPasswordPath").Value;

            await _userManager.AddToRoleAsync(user, request.Role);

            await _notificationService.NewUserAsync(user.Email, $"{setPasswordPath}?token={setPasswordToken}&userId={user.Id}");

            return Unit.Value;
        }

        private async Task<bool> CheckIfExistAsync(string email)
        {
            var userExists = await _userManager.FindByEmailAsync(email);
            if (userExists != null)
            {
                throw new BusinessException(ExceptionCodes.GetInvalidValueCode("Email"), $"User with that e-mail already exist.");
            }
            return true;
        }
    }
}
