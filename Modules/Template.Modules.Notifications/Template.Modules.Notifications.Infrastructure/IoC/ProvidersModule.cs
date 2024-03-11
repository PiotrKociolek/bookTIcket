using Autofac;
using Template.Modules.Notifications.Application.Providers;

namespace Template.Modules.Notifications.Infrastructure.IoC
{
    public class ProvidersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            builder.RegisterType<ResourceProvider>().As<IResourceProvider>().InstancePerLifetimeScope();
        }
    }
}