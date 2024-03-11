using Microsoft.Extensions.Configuration;
using Template.Modules.Shared.Core.Exceptions;

namespace Template.Modules.Shared.Application.Extensions
{
    public static class ConfigurationExtensions
    {
        public static T GetConfiguration<T>(this IConfiguration configuration) where T : new()
        {
            var section = configuration.GetSection(typeof(T).Name.Replace("Settings", string.Empty));

            if (!section.Exists())
            {
                throw new ConfigurationException($"Configuration for: '{typeof(T).Name}' is not provided."); 
            }
            
            var cfg = new T();
            
            section.Bind(cfg);

            return cfg;
        }
    }
}