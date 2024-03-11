using Template.Modules.Core.Application.Messages.Commands.Users;
using FluentValidation;

namespace Template.Api.Validators.Users
{
    public class ChangePasswordValidator : BaseValidator<ChangePassword>
    {
        public ChangePasswordValidator()
        {
            RuleFor(e => e.OldPassword).NotNull().NotEmpty().WithMessage("Please provide valid password.");
            RuleFor(e => e.NewPassword).NotNull().NotEmpty().Length(8, 50).WithMessage("Please provide valid password.");
            RuleFor(e => e.NewPasswordRepeat).NotNull().NotEmpty().Length(8, 50).WithMessage("Please provide valid password.").Equal(e => e.NewPassword);
        }
    }
}
