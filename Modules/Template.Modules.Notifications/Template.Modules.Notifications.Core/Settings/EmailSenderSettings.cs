namespace Template.Modules.Notifications.Core.Settings
{
    public class EmailSenderSettings
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string SmtpClient { get; set; }
        public int SmtpPort { get; set; }
    }
}