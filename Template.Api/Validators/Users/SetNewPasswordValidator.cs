using Template.Modules.Core.Application.Messages.Commands.Users;
using FluentValidation;

namespace Template.Api.Validators.Users
{
    public class SetNewPasswordValidator : BaseValidator<SetNewPassword>
    {
        public SetNewPasswordValidator()
        {
            RuleFor(e => e.UserId).NotNull().WithMessage("Please provide valid user id.");
            RuleFor(e => e.Key).Length(10, 500).NotNull().WithMessage("Please provide valid reset token.");
            RuleFor(e => e.Password).NotNull().NotEmpty().Length(8, 50).WithMessage("Please provide valid password.");
            RuleFor(e => e.RepeatPassword).NotNull().NotEmpty().Length(8, 50).Equal(e => e.Password).WithMessage("Please provide valid password.");
        }
    }
}
