using BookTicket.Model;
using BookTicket.Model.Dto_s.request;
using BookTicket.Model.Dtos.User;

namespace BookTicket.Services.Interfaces;

public interface IUserService
{
    Task<User> RegisterUserAsync(RegisterRequestDto dto);
    Task<string> LoginUserAsync(LoginRequestDto dto);
    Task<User> GetUserById(int userId);
    Task DeleteUserByIdAsync(int id);
}