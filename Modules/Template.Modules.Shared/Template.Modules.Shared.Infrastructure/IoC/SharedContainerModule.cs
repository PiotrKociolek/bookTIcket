using Autofac;
using Template.Modules.Shared.Application.Extensions;
using Template.Modules.Shared.Application.Services;
using Template.Modules.Shared.Application.Settings;
using Template.Modules.Shared.Application.Stores;
using Template.Modules.Shared.Infrastructure.Services;
using Template.Modules.Shared.Infrastructure.Stores;
using Microsoft.Extensions.Configuration;

namespace Template.Modules.Shared.Infrastructure.IoC
{
    public class SharedContainerModule : Module
    {
        private readonly IConfiguration _configuration;
 
        public SharedContainerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            builder.RegisterModule<HandlersModule>();

            //Settings
            builder.RegisterInstance(_configuration.GetConfiguration<SqlSettings>()).SingleInstance();
            builder.RegisterInstance(_configuration.GetConfiguration<CorsWhiteListSettings>()).SingleInstance();
            builder.RegisterInstance(_configuration.GetConfiguration<FileStoreSettings>()).SingleInstance();
            builder.RegisterInstance(_configuration.GetConfiguration<DomainSettings>()).SingleInstance();
            builder.RegisterInstance(_configuration.GetConfiguration<CookiePolicySettings>()).SingleInstance();

            //Stores
            builder.RegisterType<ImageStore>().As<IImageStore>().SingleInstance();

            //Services
            builder.RegisterType<MimeDetectionService>().As<IMimeDetectionService>().SingleInstance();
            builder.RegisterType<CsvService>().As<ICsvService>().SingleInstance();
        }
    }
}