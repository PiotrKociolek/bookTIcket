namespace Template.Modules.Shared.Application.Settings
{
    public class CorsWhiteListSettings
    {
        public bool IsEnabled { get; set; }
        public string[] AllowedOrigins { get; set; }
    }
}