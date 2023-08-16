using GamesLibraryApi.Interfaces.Repository;
using GamesLibraryApi.Interfaces.Services;
using GamesLibraryApi.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GamesLibraryApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository repository,
            IConfiguration configuration)
        {
            _userRepository = repository;
            _configuration = configuration;
        }

        public async Task<ICollection<User>> GetAllUsers() 
        {
            return await _userRepository.GetAll();
        }

        public async Task<User?> GetById(int id)
        {
            return await _userRepository.Get(id);
        }

        public async Task<bool> Add(User user)
        {
            return await _userRepository.Create(user);
        }

        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);
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

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.Integer32),
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

        public async Task<bool> AddGameToUser(int userId, int gameId)
        {
            return await _userRepository.AddGameToUser(userId, gameId);
        }

        public async Task<bool> CheckUsernameExists(string username)
        {
            return await _userRepository.CheckUsernameExists(username);
        }

        public async Task<bool> UpdateUsername(int id, string username)
        {
            return await _userRepository.UpdateUsername(id, username);
        }

        public async Task<bool> UpdatePassword(int id, string password)
        {
            return await _userRepository.UpdatePassword(id, password);
        }

        public async Task<bool> CheckEmailExists(string email)
        {
            return await _userRepository.CheckEmailExists(email);
        }

        public async Task<bool> UpdateEmail(int id, string email)
        {
            return await _userRepository.UpdateEmail(id, email);
        }

        public async Task<bool> Delete(int id)
        {
            return await _userRepository.Delete(id);
        }
    }
}
