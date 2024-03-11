using Template.Modules.Core.Application.Dto.Users;
using MediatR;

namespace Template.Modules.Core.Application.Messages.Queries.Users
{
    public class ValidateEmail : IRequest<ValidateEmailDto>
    {
        public string Email { get; set; }
    }
}
