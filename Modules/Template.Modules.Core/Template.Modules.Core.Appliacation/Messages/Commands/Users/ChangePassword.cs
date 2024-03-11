using MediatR;

namespace Template.Modules.Core.Application.Messages.Commands.Users
{
    public class ChangePassword : IRequest<Unit>
    {
        public string OldPassword { get; private set; }
        public string NewPassword { get; private set; }
        public string NewPasswordRepeat { get; private set; }

        public ChangePassword(string oldPassword, string newPassword, string newPasswordRepeat)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
            NewPasswordRepeat = newPasswordRepeat;
        }
    }
}
