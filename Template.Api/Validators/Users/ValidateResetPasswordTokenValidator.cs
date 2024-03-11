using Template.Modules.Core.Application.Messages.Queries.Users;
using FluentValidation;

namespace Template.Api.Validators.Users
{
    public class ValidateResetPasswordTokenValidator : BaseValidator<ValidateResetPasswordToken>
    {
        public ValidateResetPasswordTokenValidator()
        {
            RuleFor(e => e.UserId).NotNull().NotEmpty().WithMessage("Please provide valid user id.");
            RuleFor(e => e.Token).NotNull().NotEmpty().Length(10, 500).WithMessage("Please provide valid token.");
        }
    }
}
