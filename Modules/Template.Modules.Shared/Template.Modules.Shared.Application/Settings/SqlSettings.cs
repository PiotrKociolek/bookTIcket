namespace Template.Modules.Shared.Application.Settings
{
    public class SqlSettings
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool IsWindowsAuthentication { get; set; } = false;

        public string GetConnectionString()
        {
            if (IsWindowsAuthentication)
            {
                return $"Server={Server};Database={Database};Trusted_Connection=True;TrustServerCertificate=True;";
            }
            
            return $"Server={Server};Database={Database};User={User};Password={Password};TrustServerCertificate=True;";
        } 
    }
}