using Template.Modules.Core.Application.Messages.Commands.Users;
using FluentValidation;

namespace Template.Api.Validators.Accounts
{
    public class LogoutUserValidator : BaseValidator<LogoutUser>
    {
        public LogoutUserValidator()
        {
            RuleFor(e => e.Id).NotNull().WithMessage("Please provide valid id.");
        }
    }
}
