using Template.Modules.Core.Application.Dto.Users;
using Template.Modules.Core.Application.Messages.Queries.Users;
using Template.Modules.Shared.Application.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Template.Modules.Core.Core.Domain;

namespace Template.Modules.Core.Application.Handlers.Queries.Users
{
    public class GetUserSelfHandler : BaseHandler<GetUserSelf, UserDto>
    {
        UserManager<User> _userManager;

        public GetUserSelfHandler(
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager)
           : base(httpContextAccessor, userManager)
        {
            _userManager = userManager;
        }

        public override async Task<UserDto> Handle(GetUserSelf request, CancellationToken cancellationToken)
        {
            await base.Handle(request, cancellationToken);

            var user = await GetCurrentUserAsync();

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = new UserDto()
            {
                Id = Guid.Parse(user.Id),
                Email = user.Email,
                UserName = user.UserName,
                UserRole = userRoles.FirstOrDefault()
            };

            return result;
        }
    }
}
