using GamesLibraryApi.Data;
using GamesLibraryApi.Models.Games;
using Microsoft.EntityFrameworkCore;

namespace GamesLibraryApi.Services
{
    public class GameService
    {
        private readonly DataContext _context;

        public GameService(DataContext context)
        {
            _context = context;
        }

        public ICollection<Game> GetGames()
        {
            return _context.Games.AsNoTracking().ToList();
        }

        public Game? GetById(int id)
        {
            return _context.Games
                .Include(g => g.Genres)
                .FirstOrDefault(g => g.Id == id);
        }

        public bool Add(Game newGame)
        {
            _context.Games.Add(newGame);
            return Save();
        }

        public bool AddGenreToGame(int gameId, int genreId)
        {
            var game = _context.Games.Include(g => g.Genres)
                        .FirstOrDefault(g => g.Id == gameId);
            var genre = _context.Genres.Find(genreId);

            if(game == null || genre == null) return false;

            game.Genres.Add(genre);
            return Save();
        }

        public bool Delete(int id)
        {
            var gameToDelete = _context.Games.Find(id);
            if (gameToDelete == null) return false;
            _context.Games.Remove(gameToDelete);
            return Save();
        }

        public bool DeleteGenre(int gameId, int genreId)
        {
            var game = _context.Games.Include(g => g.Genres)
                        .FirstOrDefault(g => g.Id == gameId);
            var genre = _context.Genres.Find(genreId);

            if(game == null || genre == null) { return false; }

            game.Genres.Remove(genre);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
