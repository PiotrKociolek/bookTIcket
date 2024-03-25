using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookTicket.Data;
using BookTicket.Model;
using BookTicket.Model.Dto_s.request;
using BookTicket.Model.Flag;
using BookTicket.service;
using BookTicket.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BookTicket.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IAuthService _authService;
        private readonly AppSettings _appSettings;

        public UserService(AppDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
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

            var jwtToken = await generateJwtToken(user);

            return jwtToken;
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
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

        private async Task<string> generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}