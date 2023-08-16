using GamesLibraryApi.Models.Users;

namespace GamesLibraryApi.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetAll();
        Task<User?> Get(int id);
        Task<User?> GetUserByEmail(string email);
        Task<bool> Create(User user);
        Task<bool> AddGameToUser(int userId, int gameId);
        Task<bool> CheckUsernameExists(string username);
        Task<bool> UpdateUsername(int id, string username);
        Task<bool> UpdatePassword(int id, string password);
        Task<bool> CheckEmailExists(string email);
        Task<bool> UpdateEmail(int id, string email);
        Task<bool> Delete(int id);
        Task<bool> SaveAsync();
    }
}
