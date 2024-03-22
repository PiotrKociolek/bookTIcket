using BookTicket.Data;
using BookTicket.Model;
using BookTicket.Model.Dto_s.request;

namespace BookTicket.service;

public interface IUserService
{
    Task<User> RegisterUserAsync(RegisterRequestDto dto);
    Task<string> LoginUserAsync(LoginRequestDto dto);
    Task<User> GetUserById(int userId);
}