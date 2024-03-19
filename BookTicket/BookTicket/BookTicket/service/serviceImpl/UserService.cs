using BookTicket.Data;
using BookTicket.Model;
using BookTicket.Model.Dto_s.request;
using Microsoft.EntityFrameworkCore;

namespace BookTicket.service.serviceImpl
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IAuthService _authService;


        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterUserAsync(RegisterRequestDto dto)
        {
            var existingUser = await GetUserByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists");
            }

            var passwordHash = HashPassword(dto.Password);
            var confirmPasswordHash = HashPassword(dto.ConfirmPassword);
            if (dto.Password != dto.ConfirmPassword)
            {
                throw new Exception("Password and confirm password do not match");
            }

            var newUser = new User
            {
                Email = dto.Email,
                Password = passwordHash,
                Name = dto.Name,
                Surname = dto.Surname
            };
            SetRegularRole(newUser);


            await AddUserAsync(newUser);
            return newUser;
        }

        public async Task<string> LoginUserAsync(LoginRequestDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                throw new Exception("Invalid password");
            }

            // Generate JWT token
            var jwtToken = _authService.GenerateJwtToken(user.Id.ToString());

            return jwtToken;
        }


        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }


        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private void SetRegularRole(User user)
        {
            user.Role = Role.Regular;
        }
    }
}