using System.Reflection;

namespace Template.Modules.Notifications.Application.Providers
{
    public class ResourceProvider : IResourceProvider
    {
        public string GetResource(string key)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"Template.Modules.Notifications.Application.Resources.EmailTemplates.{key}";

            using var stream = assembly.GetManifestResourceStream(resourceName);

            if (stream is null)
            {
                throw new Exception($"Resource '{resourceName}' not found.");
            }
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}