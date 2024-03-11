using Template.Modules.Notifications.Core.Domain;

namespace Template.Modules.Notifications.Core.Senders
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Email email);
    }
}
