using GamesLibraryApi.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace GamesLibraryApi.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetAllUsers();
        Task<User?> GetById(int id);
        Task<bool> Add(User user);
        Task<IActionResult> Login(string email, string password);
        string CreateToken(User user);
        Task<bool> CheckUsernameExists(string username);
        Task<bool> UpdateUsername(int id, string username);
        Task<bool> UpdatePassword(int id, string password);
        Task<bool> CheckEmailExists(string email);
        Task<bool> UpdateEmail(int id, string email);
        Task<bool> Delete(int id);
    }
}
