namespace Template.Modules.Notifications.Application.Services
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string templateFile, object model, string emailTo, string subject, string templateKey);
        public Task SendEmailAsync(string templateFile, object model, IEnumerable<string> emailsTo, string subject, string templateKey);
    }
}
