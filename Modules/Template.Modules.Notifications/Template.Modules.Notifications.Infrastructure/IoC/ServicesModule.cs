using Autofac;
using Template.Modules.Notifications.Application.Services;
using Template.Modules.Notifications.Core.Senders;
using Template.Modules.Notifications.Core.Senders.Providers;

namespace Template.Modules.Notifications.Infrastructure.IoC
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<SmtpEmailSender>().As<IEmailSender>().InstancePerLifetimeScope();
            builder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();
            builder.RegisterType<NotificationService>().As<INotificationService>().InstancePerLifetimeScope();
        }
    }
}
