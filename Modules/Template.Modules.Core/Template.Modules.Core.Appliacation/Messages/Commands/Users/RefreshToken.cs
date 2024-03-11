using Template.Modules.Core.Application.Dto.Accounts;
using MediatR;

namespace Template.Modules.Core.Application.Messages.Commands.Users
{
    public class RefreshToken : IRequest<TokensDto>
    {
    }
}
