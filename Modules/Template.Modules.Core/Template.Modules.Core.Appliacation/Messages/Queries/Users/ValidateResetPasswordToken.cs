using Template.Modules.Core.Application.Dto.Accounts;
using MediatR;

namespace Template.Modules.Core.Application.Messages.Queries.Users
{
    public class ValidateResetPasswordToken : IRequest<ValidateResetPasswordTokenDto>
    {
        public string Token { get; set; }
        public string UserId { get; set; }
    }
}
