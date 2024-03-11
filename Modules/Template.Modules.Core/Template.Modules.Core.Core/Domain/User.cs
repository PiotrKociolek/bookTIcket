using Template.Modules.Shared.Core.Exceptions;
using Template.Modules.Shared.Core.Exceptions.Codes;
using Template.Modules.Shared.Core.Exceptions.Messages;
using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;

namespace Template.Modules.Core.Core.Domain
{
    public class User : IdentityUser
    {
        public bool IsDeleted { get; private set; }
        public IEnumerable<RefreshToken> Tokens => _tokens;
        private ICollection<RefreshToken> _tokens { get; set; } = new Collection<RefreshToken>();
        public bool ChangePassword { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private User()
        {
            /// Entity Framework constructor
        }

        public User(Guid id, string email, string userName,bool changePassword)
        {
            SetId(id);
            SetEmail(email);
            SetUserName(userName);
            SetChangePassword(changePassword);
            SetCreatedAt();
        }

        private void SetId(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new BusinessException(ExceptionCodes.GetInvalidValueCode(nameof(id)), ExceptionMessages.GetEmptyValueMessage(nameof(id)));
            }

            Id = id.ToString();
        }

        private void SetEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || email.Length > 50)
            {
                throw new BusinessException(ExceptionCodes.GetInvalidValueCode(nameof(email)), ExceptionMessages.GetInvalidValueMessage(nameof(email)));
            }

            Email = email;
        }

        public void SetUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName) || userName.Length > 50)
            {
                throw new BusinessException(ExceptionCodes.GetInvalidValueCode(nameof(userName)), ExceptionMessages.GetInvalidValueMessage(nameof(userName)));
            }

            UserName = userName;
        }

        public void DeleteUser()
        { 
            IsDeleted = true;
        }

        public void AddRefreshToken(RefreshToken token)
        {
            if (token is null)
            {
                throw new BusinessException(ExceptionCodes.GetInvalidValueCode(nameof(token)), ExceptionMessages.GetNullValueMessage(nameof(token)));
            }

            _tokens.Add(token);
        }

        public void RemoveRefreshToken(RefreshToken token)
        {
            if (token is null)
            {
                throw new BusinessException(ExceptionCodes.GetInvalidValueCode(nameof(token)), ExceptionMessages.GetNullValueMessage(nameof(token)));
            }

            _tokens.Remove(token);
        }

        public void SetChangePassword(bool changePassword)
        {
            ChangePassword = changePassword;
        }

        private void SetCreatedAt()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
