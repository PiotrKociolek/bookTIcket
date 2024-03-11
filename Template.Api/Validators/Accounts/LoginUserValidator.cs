using Template.Modules.Core.Application.Messages.Commands.Users;
using FluentValidation;

namespace Template.Api.Validators.Accounts
{
    public class LoginUserValidator : BaseValidator<LoginUser>
    {
        public LoginUserValidator()
        {
            RuleFor(e => e.Email).NotNull().WithMessage("Please provide valid email.");
            RuleFor(e => e.Password).NotNull().NotEmpty().Length(8, 50).WithMessage("Please provide valid password.");
        }
    }
}
