using Template.Modules.Core.Application.EF;
using Template.Modules.Core.Application.Messages.Commands.Users;
using Template.Modules.Core.Core.Domain;
using Template.Modules.Shared.Application.Extensions;
using Template.Modules.Shared.Application.Handlers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Template.Modules.Core.Application.Handlers.Commands.Users
{
    public class EditUserHandler : BaseHandler<EditUser, Unit>
    {
        private readonly TemplateContext _context;

        public EditUserHandler(
           TemplateContext context,
           IHttpContextAccessor httpContextAccessor,
           UserManager<User> userManager)
           : base(httpContextAccessor, userManager)
        {
            _context = context;
        }

        public override async Task<Unit> Handle(EditUser request, CancellationToken cancellationToken)
        {
            await base.Handle(request, cancellationToken);

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == request.Id.ToString())
                .OrFail("user_not_found", $"User with Id '{request.Id}' not found");

            user.SetUserName(request.UserName);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
