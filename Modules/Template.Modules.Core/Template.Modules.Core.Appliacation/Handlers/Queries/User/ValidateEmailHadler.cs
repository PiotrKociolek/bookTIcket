using Template.Modules.Core.Application.Dto.Users;
using Template.Modules.Core.Application.Messages.Queries.Users;
using Template.Modules.Shared.Application.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Template.Modules.Core.Core.Domain;

namespace Template.Modules.Core.Application.Handlers.Queries.Users
{
    public class ValidateEmailHadler : BaseHandler<ValidateEmail, ValidateEmailDto>
    {
        private readonly UserManager<User> _userManager;

        public ValidateEmailHadler(
           IHttpContextAccessor httpContextAccessor,
           UserManager<User> userManager)
           : base(httpContextAccessor, userManager)
        {
            _userManager = userManager;
        }

        public override async Task<ValidateEmailDto> Handle(ValidateEmail request, CancellationToken cancellationToken)
        {
            await base.Handle(request, cancellationToken);
            var response = new ValidateEmailDto();

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.EmailExist = false;
            }
            else
            {
                response.EmailExist = true;
            }

            return response;
        }
    }
}
