namespace Template.Modules.Shared.Application.Settings
{
    public class RestEaseSettings
    {
        public List<Services> Services { get; set; }
    }

    public class Services
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
