using GamesLibraryApi.Data;
using GamesLibraryApi.Interfaces.Repository;
using GamesLibraryApi.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace GamesLibraryApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<User>> GetAll()
        {
            var users = await _context.Users.AsNoTracking().ToListAsync();
            return users;
        }

        public async Task<User?> Get(int id)
        {
            var user = await _context.Users
                .Include(u => u.UserGamePurchases).AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await _context.Users.
                FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<bool> Create(User user)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = passwordHash;
            await _context.Users.AddAsync(user);
            return await SaveAsync();
        }

        public async Task<bool> AddGameToUser(int userId, int gameId)
        {
            var user = await _context.Users.FindAsync(userId);
            var game = await _context.Games.FindAsync(gameId);

            if (user == null || game == null) return false;

            var gamePurchase = new UserGamePurchase
            {
                UserId = userId,
                GameId = gameId,
                PurchasePrice = game.Price,
            };

            _context.UserGamePurchases.Add(gamePurchase);
            return await SaveAsync();
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
            if (user == null) return false;
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
