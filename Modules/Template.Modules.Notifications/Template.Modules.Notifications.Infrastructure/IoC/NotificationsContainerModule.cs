using Autofac;
using Microsoft.Extensions.Configuration;
using Template.Modules.Notifications.Core.Settings;
using Template.Modules.Shared.Application.Extensions;

namespace Template.Modules.Notifications.Infrastructure.IoC
{
    public class NotificationsContainerModule : Module
    {
        private readonly IConfiguration _configuration;
 
        public NotificationsContainerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<ServicesModule>();

            builder.RegisterModule<ProvidersModule>();

            builder.RegisterInstance(_configuration.GetConfiguration<EmailSenderSettings>()).SingleInstance();
        } 
    }
}