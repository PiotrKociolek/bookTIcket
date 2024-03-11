using Autofac;
using Template.Modules.Core.Application.Services;

namespace Template.Modules.Core.Infrastructure.IoC
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();
        }
    }
}
