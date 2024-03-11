using Template.Modules.Core.Application.Messages.Commands.Users;
using FluentValidation;

namespace Template.Api.Validators.Users
{
    public class ResetPasswordValidator : BaseValidator<ResetPassword>
    {
        public ResetPasswordValidator()
        {
            RuleFor(e => e.Email).NotNull().NotEmpty().EmailAddress().WithMessage("Please provide valid email address.");
        }
    }
}
