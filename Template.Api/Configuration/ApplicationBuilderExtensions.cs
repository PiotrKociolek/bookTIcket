using Template.Modules.Shared.Infrastructure.Framework.Middleware;
using Template.Modules.Shared.Infrastructure.Middleware;

namespace Template.Api.Configuration
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddWebApiConfigurations(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
          
            app.UseCors("AllowAll");

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMiddleware<AuthorizationRoleMiddleware>();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            return app;
        }
    }
}