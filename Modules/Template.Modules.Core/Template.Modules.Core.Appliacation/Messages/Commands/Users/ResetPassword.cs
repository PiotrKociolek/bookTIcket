using MediatR;

namespace Template.Modules.Core.Application.Messages.Commands.Users
{
    public class ResetPassword : IRequest<Unit>
    {
        public string Email { get; private set; }

        public ResetPassword(string email)
        {
            Email = email;
        }
    }
}
