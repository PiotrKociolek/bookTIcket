using Template.Modules.Core.Application.Messages.Commands.Users;
using FluentValidation;

namespace Template.Api.Validators.Users
{
    public class DeleteUserValidator : BaseValidator<DeleteUser>
    {
        public DeleteUserValidator()
        {
            RuleFor(e => e.Id).NotNull().WithMessage("Please provide valid id.");
        }
    }
}
