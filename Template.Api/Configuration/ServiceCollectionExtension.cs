using Template.Modules.Core.Application.EF;
using Template.Modules.Core.Application.Mappers;
using Template.Modules.Shared.Infrastructure.Extensions;
using Template.Modules.Shared.Infrastructure.Middleware;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Template.Api.Configuration
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddWebApiConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.ConfigureCookiePolicy(configuration);

            services.AddSwagger();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(new[] { Assembly.GetExecutingAssembly(), typeof(CoreProfile).Assembly }));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviourMiddleware<,>));
            services.AddSerilog();

            services.AddDbContext<TemplateContext>();

            services.AddAuthentication(configuration);

            services.AddMemoryCache();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();

            services.AddCors(configuration);

            return services;
        }
    }
}
