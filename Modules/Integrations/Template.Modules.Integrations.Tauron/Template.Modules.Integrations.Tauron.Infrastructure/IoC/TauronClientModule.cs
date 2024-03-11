using Autofac;
using Template.Modules.Integrations.Tauron.Application.Services;
using Microsoft.Extensions.Configuration;

namespace Template.Modules.Integrations.Tauron.Infrastructure.IoC
{
    public class TauronClientModule : Module
    {
        private readonly IConfiguration _configuration;
 
        public TauronClientModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<TauronService>().As<ITauronService>().InstancePerLifetimeScope();
        }
    }
}
