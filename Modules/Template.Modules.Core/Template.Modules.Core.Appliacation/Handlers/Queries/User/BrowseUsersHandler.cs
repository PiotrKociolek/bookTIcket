using AutoMapper;
using Template.Modules.Core.Application.Dto.Users;
using Template.Modules.Core.Application.EF;
using Template.Modules.Core.Application.Messages.Queries.Users;
using Template.Modules.Core.Core.Domain;
using Template.Modules.Shared.Application.Handlers;
using Template.Modules.Shared.Application.Types;
using Template.Modules.Shared.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Template.Modules.Core.Application.Handlers.Queries.Users
{
    public class BrowseUsersHandler : BaseHandler<BrowseUsers, PagedResults<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly TemplateContext _context;
        private readonly UserManager<User> _userManager;

        public BrowseUsersHandler(
           IHttpContextAccessor httpContextAccessor,
           UserManager<User> userManager,
           IMapper mapper,
           TemplateContext context)
           : base(httpContextAccessor, userManager)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }

        public override async Task<PagedResults<UserDto>> Handle(BrowseUsers request, CancellationToken cancellationToken)
        {
            await base.Handle(request, cancellationToken);

            var query = _context.Users.AsQueryable<User>();

            query = FilterData(request, query);

            var filteredUserCount = await query.CountAsync();

            query = OrderData(request, query);

            query = query
                .Skip(request.Skip)
                .Take(request.Rows);

            var users = await query.ToListAsync();

            var usersWithRoles = await GetUsersWithRolesAsync(users);

            return new PagedResults<UserDto>(request.Skip, request.Rows, filteredUserCount, usersWithRoles);
        }

        private IQueryable<User> OrderData(BrowseUsers query, IQueryable<User> resultQuery)
        {

            if (string.IsNullOrWhiteSpace(query.SortBy))
            {
                return resultQuery;
            }

            if (query.SortBy.Equals("Email", StringComparison.InvariantCultureIgnoreCase))
            {
                resultQuery = query.SortDirection == SortDirectionEnum.Asc
                    ? resultQuery.OrderBy(x => x.Email)
                    : resultQuery.OrderByDescending(x => x.Email);
            }

            return resultQuery;
        }

        private IQueryable<User> FilterData(BrowseUsers request, IQueryable<User> baseQuery)
        {
            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                baseQuery = baseQuery
                    .Where(x => x.Email.Contains(request.Email));
            }
            return baseQuery.Where(x => x.IsDeleted == false);
        }


        private async Task<List<UserDto>> GetUsersWithRolesAsync(ICollection<User> users)
        {
            var usersWithRoles = new List<UserDto>();

            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                usersWithRoles.Add(new UserDto()
                {
                    Id = Guid.Parse(user.Id),
                    Email = user.Email,
                    UserName = user.UserName,
                    UserRole = userRoles.FirstOrDefault()
                });
            }

            return usersWithRoles;
        }
    }
}
