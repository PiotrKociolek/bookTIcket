using Template.Modules.Core.Application.EF;
using Template.Modules.Core.Core.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Template.Api.Configuration
{
    public static class AuthenticationExtensions
    {
        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DataProtectionTokenProviderOptions>(o =>
            {
                o.TokenLifespan = TimeSpan.FromHours(configuration.GetValue<int>("JWT:TokenLifespanInHours"));
            });

            services.AddDataProtection()
                .PersistKeysToDbContext<TemplateContext>();
            
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<TemplateContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(configuration.GetSection("JWT:Secret").Value!)),
                    ValidIssuer = configuration.GetSection("JWT:ValidIssuer").Value,
                    ValidAudience = configuration.GetSection("JWT:ValidAudience").Value,
                    ClockSkew = TimeSpan.FromSeconds(3)
                };
            });
        }
    }
}