namespace BookTicket.Services.Interfaces;

public interface IAuthService
{
    string GenerateJwtToken(string userId);
}