using Template.Modules.Shared.Application.Extensions;
using Template.Modules.Shared.Application.Settings;

namespace Template.Api.Configuration
{
    public static class CorsConfiguration
    {
        public static IServiceCollection AddCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(x =>
            {
                x.AddPolicy("AllowAll", policyBuilder =>
                {
                    var corsSettings = configuration.GetConfiguration<CorsWhiteListSettings>();
                    
                    policyBuilder.AllowAnyHeader();
                    
                    if (corsSettings.IsEnabled)
                    {
                        if (corsSettings.AllowedOrigins != null)
                        {
                            policyBuilder.WithOrigins(corsSettings.AllowedOrigins.ToArray());   
                        }
                        policyBuilder.AllowCredentials();
                    }
                    else
                    {
                        policyBuilder.AllowAnyOrigin();
                    }

                    policyBuilder.WithMethods("GET", "POST", "DELETE", "PUT");
                });
            });

            return services;
        }
    }
}