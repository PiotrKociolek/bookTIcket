using Autofac;
using Template.Modules.Shared.Infrastructure.Framework.Exceptions;

namespace Template.Modules.Shared.Infrastructure.IoC
{
    public class HandlersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            builder.RegisterType<ExceptionHandler>().As<IExceptionHandler>().InstancePerLifetimeScope();
        }
    }
}