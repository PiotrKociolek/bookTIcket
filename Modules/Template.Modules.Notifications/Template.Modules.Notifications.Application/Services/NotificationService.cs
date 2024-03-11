using Template.Modules.Notifications.Application.Dto.EmailModels;

namespace Template.Modules.Notifications.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailService _emailService;

        public NotificationService(
            IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task NewUserAsync(string recipient, string link)
        {
            var emailModel = new NewUserEmailModelDto()
            {
                Link = link
            };

            await _emailService.SendEmailAsync(
                "NewUserEmailTemplate.cshtml",
                emailModel,
                recipient,
                "[Template] Dodano nowego u�ytkownika",
                "NewUserEmailTemplate");
        }

        public async Task ResetPasswordAsync(string recipient, string link)
        {
            var emailModel = new ResetPasswordEmailModelDto()
            {
                Link = link
            };

            await _emailService.SendEmailAsync(
                "PasswordResetEmailTemplate.cshtml",
                emailModel,
                recipient,
                "[Template] Reset has�a",
                "PasswordResetEmailTemplate");
        }

        public async Task PasswordChangedAsync(string recipient)
        {
            var emailModel = new PasswordChangedEmailModel();

            await _emailService.SendEmailAsync(
                    "PasswordChangedEmailTemplate.cshtml",
                    emailModel,
                    recipient,
                    "[Template] Zmiana has�a",
                    "PasswordChangedEmailTemplate");
        }

        public async Task ExternalServiceFailureAsync(string recipient, ExternalServiceFailureModelDto model)
        {
            await _emailService.SendEmailAsync(
                    "ExternalServiceFailureTemplate.cshtml",
                    model,
                    recipient,
                    "[Template] B�ad zewn�trznego dostawcy",
                    "ExternalServiceFailureTemplate");
        }
    }
}