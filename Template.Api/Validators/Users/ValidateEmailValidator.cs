using Template.Modules.Core.Application.Messages.Queries.Users;
using FluentValidation;

namespace Template.Api.Validators.Users
{
    public class ValidateEmailValidator : BaseValidator<ValidateEmail>
    {
        public ValidateEmailValidator()
        {
            RuleFor(e => e.Email).NotNull().NotEmpty().EmailAddress().WithMessage("Please provide valid email address.");
        }
    }
}
