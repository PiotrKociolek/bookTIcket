using Template.Modules.Core.Application.Dto.Users;
using MediatR;

namespace Template.Modules.Core.Application.Messages.Queries.Users
{
    public class GetUser : IRequest<UserDto>
    {
        public Guid Id { get; set; }

        public GetUser(Guid id)
        {
            Id = id;
        }
    }
}
