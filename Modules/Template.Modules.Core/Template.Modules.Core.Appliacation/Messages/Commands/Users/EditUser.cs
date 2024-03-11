using MediatR;

namespace Template.Modules.Core.Application.Messages.Commands.Users
{
    public class EditUser : IRequest<Unit>
    {
        public Guid Id { get; private set; }
        public string UserName { get; private set; }

        public EditUser(Guid id, string userName)
        {
            Id = id;
            UserName = userName;
        }
    }
}
