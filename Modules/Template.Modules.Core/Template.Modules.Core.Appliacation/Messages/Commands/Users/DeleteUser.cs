using MediatR;

namespace Template.Modules.Core.Application.Messages.Commands.Users
{
    public class DeleteUser : IRequest<Unit>
    {
        public Guid Id { get; private set; }

        public DeleteUser(Guid id)
        {
            Id = id;
        }
    }
}
