namespace Template.Modules.Notifications.Application.Dto.EmailModels
{
    public class ExternalServiceFailureModelDto
    {
        public string Provider { get; set; }
        public ICollection<string> ErrorMessages { get; set; }
        public string StatusCode { get; set; }
        public string Content { get; set; }
    }
}
