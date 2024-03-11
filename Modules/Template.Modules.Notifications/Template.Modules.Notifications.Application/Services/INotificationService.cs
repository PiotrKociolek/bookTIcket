using Template.Modules.Notifications.Application.Dto.EmailModels;

namespace Template.Modules.Notifications.Application.Services
{
    public interface INotificationService
    {
        Task NewUserAsync(string recipient, string link);
        Task ResetPasswordAsync(string recipient, string link);
        Task PasswordChangedAsync(string recipient);
        Task ExternalServiceFailureAsync(string recipient, ExternalServiceFailureModelDto model);
    }
}