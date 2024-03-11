namespace Template.Modules.Core.Application.Dto.Users
{
    public class LoggedUserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
    }
}
