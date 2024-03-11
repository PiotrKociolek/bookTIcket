using MediatR;

namespace Template.Modules.Core.Application.Messages.Commands.Users
{
    public class CreateUser : IRequest<Unit>
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string UserName { get; private set; }
        public Guid OrganizationId { get; private set; }
        public string Role { get; private set; }

        public CreateUser(string email, string userName, Guid organizationId, string role)
        {
            Email = email;
            UserName = userName;
            OrganizationId = organizationId;
            Role = role;
        }
    }
}
