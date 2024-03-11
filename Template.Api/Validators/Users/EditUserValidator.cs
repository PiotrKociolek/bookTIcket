using Template.Modules.Core.Application.Messages.Commands.Users;
using FluentValidation;

namespace Template.Api.Validators.Users
{
    public class EditUserValidator : BaseValidator<EditUser>
    {
        public EditUserValidator()
        {
            RuleFor(e => e.Id).NotNull().WithMessage("Please provide valid id.");
            RuleFor(e => e.UserName).NotNull().NotEmpty().Length(3, 50).WithMessage("Please provide valid user name.");
        }
    }
}
