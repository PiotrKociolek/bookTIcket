using Autofac;
using Microsoft.Extensions.Configuration;

namespace Template.Modules.Core.Infrastructure.IoC
{
    public class CoreContainerModule : Module
    {
        private readonly IConfiguration _configuration;
 
        public CoreContainerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<ServicesModule>();
        } 
    }
}