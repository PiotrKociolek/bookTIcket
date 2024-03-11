using MediatR;

namespace Template.Modules.Core.Application.Messages.Commands.Users
{
    public class LogoutUser : IRequest<Unit>
    {
        public Guid Id { get; private set; }

        public LogoutUser(Guid id)
        {
            Id = id;
        }
    }
}
