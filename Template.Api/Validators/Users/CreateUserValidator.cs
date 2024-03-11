using Template.Modules.Core.Application.Messages.Commands.Users;
using FluentValidation;

namespace Template.Api.Validators.Users
{
    public class CreateUserValidator : BaseValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(user => user.Id).NotNull().WithMessage("Please provide user id.");
            RuleFor(user => user.Email).NotNull().NotEmpty().EmailAddress().Length(3, 50).WithMessage("Please provide valid user email.");
            RuleFor(user => user.UserName).NotNull().NotEmpty().Length(3, 50).WithMessage("Please provide valid user name.");
            RuleFor(user => user.OrganizationId).NotNull().WithMessage("Please provide organization id.");
            RuleFor(user => user.Role).NotNull().Must(ValidUserRole).NotEmpty().Length(3, 20).WithMessage("Please provide valid user role.");
        }
    }
}
