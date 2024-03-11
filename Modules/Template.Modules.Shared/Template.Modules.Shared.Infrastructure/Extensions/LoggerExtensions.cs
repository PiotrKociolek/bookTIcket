using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Exceptions;

namespace Template.Modules.Shared.Infrastructure.Extensions
{
    public static class LoggerExtensions
    {
        public static void AddSerilog(this IServiceCollection services)
        {
            Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));

            Log.Logger = new LoggerConfiguration()
                .Enrich.WithExceptionDetails()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/template.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}