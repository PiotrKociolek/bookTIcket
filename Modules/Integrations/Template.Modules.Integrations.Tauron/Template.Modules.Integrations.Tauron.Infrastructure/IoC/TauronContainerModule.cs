using Autofac;
using Microsoft.Extensions.Configuration;

namespace Template.Modules.Integrations.Tauron.Infrastructure.IoC
{
    public class TauronContainerModule : Module
    {
        private readonly IConfiguration _configuration;

        public TauronContainerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule(new TauronClientModule(_configuration));
        }
    }
}
