using Template.Modules.Notifications.Application.Providers;
using Template.Modules.Notifications.Core.Domain;
using Template.Modules.Notifications.Core.Senders;
using Template.Modules.Notifications.Core.Settings;
using RazorEngine;
using RazorEngine.Templating;

namespace Template.Modules.Notifications.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSender _smtpEmailSender;
        private readonly IResourceProvider _resourceProvider;
        private readonly EmailSenderSettings _emailSenderSettings;

        public EmailService(
            IEmailSender smtpEmailSender, 
            IResourceProvider resourceProvider,
            EmailSenderSettings emailSenderSettings) 
        {
            _smtpEmailSender = smtpEmailSender;
            _resourceProvider = resourceProvider;
            _emailSenderSettings = emailSenderSettings;
        }

        public async Task SendEmailAsync(string templateFile, object model, string emailTo, string subject, string templateKey)
        {
            var emailFrom = _emailSenderSettings.Email;

            var template = _resourceProvider.GetResource(templateFile);
            var result = Engine.Razor.RunCompile(template, templateKey, null, model);

            var email = new Email(emailFrom, emailTo, subject, result);

            await _smtpEmailSender.SendEmailAsync(email);
        }

        public async Task SendEmailAsync(string templateFile, object model, IEnumerable<string> emailsTo, string subject, string templateKey)
        {
            foreach (var emailTo in emailsTo)
            {
                await SendEmailAsync(templateFile, model, emailTo, subject, templateKey);
            }
        }
    }
}
