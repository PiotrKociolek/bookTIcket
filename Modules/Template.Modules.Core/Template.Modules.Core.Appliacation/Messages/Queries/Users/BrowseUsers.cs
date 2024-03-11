using Template.Modules.Core.Application.Dto.Users;
using Template.Modules.Shared.Application.Types;
using Template.Modules.Shared.Core.Enums;
using MediatR;

namespace Template.Modules.Core.Application.Messages.Queries.Users
{
    public class BrowseUsers : IRequest<PagedResults<UserDto>>
    {
        public int Skip { get; set; }
        public int Rows { get; set; }
        public string Email { get; set; }
        public SortDirectionEnum SortDirection { get; set; }
        public string SortBy { get; set; }
    }
}
