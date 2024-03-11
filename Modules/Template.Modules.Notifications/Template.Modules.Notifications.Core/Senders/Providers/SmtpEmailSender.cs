using FluentEmail.Smtp;
using Template.Modules.Notifications.Core.Domain;
using Polly;
using Serilog;
using System.Net;
using System.Net.Mail;
using Template.Modules.Notifications.Core.Settings;

namespace Template.Modules.Notifications.Core.Senders.Providers
{
    public class SmtpEmailSender: IEmailSender
    {
        private readonly EmailSenderSettings _emailSenderSettings;

        public SmtpEmailSender(EmailSenderSettings emailSenderSettings)
        {
            _emailSenderSettings = emailSenderSettings;
        }

        public async Task SendEmailAsync(Email email)
        {
            string senderEmail = _emailSenderSettings.Email;
            string emailPassword = _emailSenderSettings.Password;
            string smtpClient = _emailSenderSettings.SmtpClient;
            int smtpPort = _emailSenderSettings.SmtpPort;

            var sender = new SmtpSender(() => new SmtpClient(smtpClient, smtpPort)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail, emailPassword),
                EnableSsl = true
            });

            var emailToSend = FluentEmail.Core.Email
                .From(email.From)
                .To(email.To)
                .Subject(email.Subject)
                .UsingTemplate(email.Template, new { });

            await TrySendEmailAsync(email, sender, emailToSend);
        }

        public async Task TrySendEmailAsync(Email email, SmtpSender sender, FluentEmail.Core.IFluentEmail emailToSend)
        {
            await Policy
            .Handle<SmtpException>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
            (exception, span) => { HandleError(email, exception); })
            .ExecuteAsync(async () =>
            {
                await sender.SendAsync(emailToSend);
            });
        }

        private void HandleError(Email email, Exception exception)
        {
            Log.Logger.Error(exception.Message);
            Log.Logger.Warning(
                $"Email to '{email.To}' with subject '{email.Subject}' not sent. Retrying...");
        }
    }
}
