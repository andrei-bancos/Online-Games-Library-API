using GamesLibraryApi.Data;
using GamesLibraryApi.Interfaces;
using GamesLibraryApi.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GamesLibraryApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public UserRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ICollection<User>> GetAllUsers() 
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> Add(User user)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = passwordHash;
            await _context.Users.AddAsync(user);
            return await SaveAsync();
        }

        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.Users.
                FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) 
                return new ObjectResult("Email or password are wrong.") 
                       { StatusCode = 401 };

            var checkPassword = BCrypt.Net.BCrypt
                .Verify(password, user.Password);
            if(!checkPassword)
                return new ObjectResult("Email or password are wrong.")
                { StatusCode = 401 };
            string jwt = CreateToken(user);
            return new OkObjectResult(jwt);
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Aud, "GamesLibrary")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("SecuritySettings:Token").Value!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(12),
                    signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }

        public async Task<bool> CheckUsernameExists(string username)
        {
            var exists = await _context.Users
                .AnyAsync(u => u.Username.Trim().ToLower() 
                == username.TrimEnd().ToLower());
            return exists;
        }

        public async Task<bool> UpdateUsername(int id, string username)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            user.Username = username;
            return await SaveAsync();
        }

        public async Task<bool> UpdatePassword(int id, string password)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null) return false;
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            user.Password = passwordHash;
            return await SaveAsync();
        }

        public async Task<bool> CheckEmailExists(string email)
        {
            var exists = await _context.Users
                .AnyAsync(u => u.Email.Trim().ToLower()
                == email.TrimEnd().ToLower());
            return exists;
        }

        public async Task<bool> UpdateEmail(int id, string email)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            user.Email = email;
            return await SaveAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            _context.Users.Remove(user);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
