using AutoMapper;
using Template.Modules.Core.Application.Dto.Users;
using Template.Modules.Core.Application.Messages.Queries.Users;
using Template.Modules.Core.Core.Domain;
using Template.Modules.Shared.Application.Extensions;
using Template.Modules.Shared.Application.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Template.Modules.Core.Application.Handlers.Queries.Users
{
    public class GetUserHandler : BaseHandler<GetUser, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public GetUserHandler(
           IHttpContextAccessor httpContextAccessor,
           UserManager<User> userManager,
           IMapper mapper)
           : base(httpContextAccessor, userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public override async Task<UserDto> Handle(GetUser request, CancellationToken cancellationToken)
        {
            await base.Handle(request, cancellationToken);

            var user = await _userManager
                .FindByIdAsync(request.Id.ToString())
                .OrFail("user_not_found", $"User with id '{request.Id}' not found");

            return _mapper.Map<UserDto>(user);
        }
    }
}
