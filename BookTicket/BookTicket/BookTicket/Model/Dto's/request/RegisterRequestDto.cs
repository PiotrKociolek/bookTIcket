using System.ComponentModel.DataAnnotations;

namespace BookTicket.Model.Dto_s.request;

public class RegisterRequestDto
{
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [Required]
    public string Email { get; set; }

    [Required] public string Password { get; set; }
    [Required] public string ConfirmPassword { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string Surname { get; set; }
}