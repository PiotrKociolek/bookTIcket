using Microsoft.AspNetCore.Http;

namespace Template.Modules.Shared.Application.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetRemoteIpAddress(this HttpContext context)
        {
            return context?.Connection?.RemoteIpAddress?.ToString();
        }

        public static string GetRemoteIpAddressFromXForwarded(this HttpContext context)
        {
            return context?.Request?.Headers["X-Forwarded-For"];
        }

        public static string GetBearerToken(this HttpContext context)
        {
            return context?.Request?.Headers["Authorization"];
        }
        
        public static string GetReferer(this HttpContext context)
        {
            return context?.Request?.Headers["Referer"];
        }
    }
}