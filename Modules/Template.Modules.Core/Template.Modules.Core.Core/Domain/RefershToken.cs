using Template.Modules.Shared.Core.Exceptions.Codes;
using Template.Modules.Shared.Core.Exceptions.Messages;
using Template.Modules.Shared.Core.Exceptions;

namespace Template.Modules.Core.Core.Domain
{
    public class RefreshToken
    {
        public Guid Id { get; private set; }
        public string UserId { get; private set; }
        public User User { get; private set; }
        public string Token { get; private set; }
        public DateTime ExpiresIn { get; private set; }

        private RefreshToken()
        {
            //entity framework constructor
        }

        public RefreshToken(Guid id, string token, User user, DateTime expiresIn)
        {
            SetId(id);
            SetUser(user);
            SetToken(token);
            SetExpiresIn(expiresIn);
        }

        private void SetId(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new BusinessException(ExceptionCodes.GetInvalidValueCode(nameof(id)), ExceptionMessages.GetEmptyValueMessage(nameof(id)));
            }

            Id = id;
        }

        private void SetUser(User user)
        {
            if (user == null)
            {
                throw new BusinessException(ExceptionCodes.GetInvalidValueCode(nameof(user)), ExceptionMessages.GetNullValueMessage(nameof(user)));
            }

            UserId = user.Id;
            User = user;
        }

        private void SetToken(string token)
        {
            if (token is null)
            {
                throw new BusinessException(ExceptionCodes.GetInvalidValueCode(nameof(token)), ExceptionMessages.GetNullValueMessage(nameof(token)));
            }

            Token = token;
        }

        private void SetExpiresIn(DateTime expiresIn)
        {
            if (expiresIn <= DateTime.UtcNow)
            {
                throw new BusinessException(ExceptionCodes.GetInvalidValueCode(nameof(expiresIn)), "Expiration date cannot be in the past.");
            }

            ExpiresIn = expiresIn;
        }
    }
}
