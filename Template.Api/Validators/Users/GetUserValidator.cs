using Template.Modules.Core.Application.Messages.Queries.Users;
using FluentValidation;

namespace Template.Api.Validators.Users
{
    public class GetUserValidator : BaseValidator<GetUser>
    {
        public GetUserValidator()
        {
            RuleFor(e => e.Id).NotNull().WithMessage("Please provide valid id.");
        }
    }
}
