using Template.Modules.Core.Application.Messages.Queries.Users;
using FluentValidation;

namespace Template.Api.Validators.Users
{
    public class BrowseUsersValidator : BaseValidator<BrowseUsers>
    {
        public BrowseUsersValidator()
        {
            RuleFor(e => e.Skip).NotNull().WithMessage("Please provide valid property value.");
            RuleFor(e => e.Rows).NotNull().InclusiveBetween(0, 100).WithMessage("Please provide valid property value.");
            RuleFor(e => e.Email).MaximumLength(100).WithMessage("Email can not be longer than 100 characters.");
            RuleFor(e => e.SortDirection).NotNull().IsInEnum().WithMessage("Please provide valid sort direction.");
            RuleFor(e => e.SortBy).NotNull().NotEmpty().WithMessage("Please provide property value.");
        }
    }
}
