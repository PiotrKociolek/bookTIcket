using Template.Modules.Core.Core.Domain;

namespace Template.Modules.Core.Application.Services
{
    public interface ITokenService
    {
        string GenerateJwtToken(string email, string id);
        RefreshToken GenerateRefreshToken(User user);
    }
}
