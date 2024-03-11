using Template.Modules.Shared.Application.Extensions;
using Template.Modules.Shared.Application.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Template.Modules.Shared.Infrastructure.Extensions
{
    public static class CookiePolicyExtensions
    {
        public static void ConfigureCookiePolicy(this IServiceCollection services, IConfiguration configuration)
        {
            var cookiePolicySettings = configuration.GetConfiguration<CookiePolicySettings>();
            
            services.Configure<CookiePolicyOptions>(options => {
                options.HttpOnly = cookiePolicySettings.HttpOnly.GetEnumValue<HttpOnlyPolicy>();
                options.Secure = cookiePolicySettings.Secure.GetEnumValue<CookieSecurePolicy>();
                options.MinimumSameSitePolicy = cookiePolicySettings.SameSite.GetEnumValue<SameSiteMode>();
            });
        }
    }
}