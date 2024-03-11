using Template.Modules.Core.Application.Dto.Users;
using MediatR;

namespace Template.Modules.Core.Application.Messages.Commands.Users
{
    public class LoginUser : IRequest<LoggedUserDto>
    {
        public string Email { get; private set; }
        public string Password { get; private set; }

        public LoginUser(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
