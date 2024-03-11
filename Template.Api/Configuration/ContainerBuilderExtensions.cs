using Autofac;
using Template.Api.Mappers;
using Template.Modules.Core.Infrastructure.IoC;
using Template.Modules.Shared.Infrastructure.IoC;
using Template.Modules.Notifications.Infrastructure.IoC;

namespace Template.Api.Configuration
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder AddWebApiConfigurations(this ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterModule(new SharedContainerModule(configuration));
            builder.RegisterModule(new CoreContainerModule(configuration));
            builder.RegisterModule(new NotificationsContainerModule(configuration));

            // Automapper
            builder.RegisterInstance(AutoMapperConfiguration.Initialize()).SingleInstance();

            return builder;
        }
    }
}