using MediatR;

namespace Template.Modules.Core.Application.Messages.Commands.Users
{
    public class SetNewPassword : IRequest<Unit>
    {
        public Guid UserId { get; private set; }
        public string Key { get; private set; }
        public string Password { get; private set; }
        public string RepeatPassword { get; private set; }

        public SetNewPassword(string key, string password, string repeatPassword, Guid userId)
        {
            Key = key;
            Password = password;
            RepeatPassword = repeatPassword;
            UserId = userId;
        }
    }
}
