using GamesLibraryApi.Data;
using GamesLibraryApi.Models.Games;
using Microsoft.EntityFrameworkCore;
using GamesLibraryApi.Interfaces;

namespace GamesLibraryApi.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly DataContext _context;

        public GameRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Game>> GetGames()
        {
            return await _context.Games.AsNoTracking().ToListAsync();
        }

        public async Task<Game?> GetById(int id)
        {
            return await _context.Games
                .Include(g => g.Genres)
                .Include(g => g.Tags)
                .Include(g => g.Media)
                .Include(g => g.CompatibilitySystems)
                .Include(g => g.Languages)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<bool> CheckGameExists(string name)
        {
            return await _context.Games
            .AnyAsync(g => g.Name.Trim().ToLower() == name.TrimEnd().ToLower());
        }

        public async Task<bool> Add(Game newGame)
        {
            await _context.Games.AddAsync(newGame);
            return await SaveAsync();
        }

        public async Task<bool> AddGenreToGame(int gameId, int genreId)
        {
            var game = await _context.Games.Include(g => g.Genres)
                        .FirstOrDefaultAsync(g => g.Id == gameId);
            var genre = await _context.Genres.FindAsync(genreId);

            if(game == null || genre == null) return false;

            game.Genres.Add(genre);
            return await SaveAsync();
        }

        public async Task<bool> AddTagToGame(int gameId, int tagId)
        {
            var game = await _context.Games.Include(g => g.Tags)
                        .FirstOrDefaultAsync(g => g.Id == gameId);
            var tag = await _context.Tags.FindAsync(tagId);

            if (game == null || tag == null) return false;

            game.Tags.Add(tag);
            return await SaveAsync();
        }

        public async Task<bool> AddSystemToGame(int gameId, int systemId)
        {
            var game = await _context.Games.Include(g => g.CompatibilitySystems)
                        .FirstOrDefaultAsync(g => g.Id == gameId);
            var system = await _context.CompatibilySystems.FindAsync(systemId);

            if (game == null || system == null) return false;

            game.CompatibilitySystems.Add(system);
            return await SaveAsync();
        }

        public async Task<bool> AddLanguageToGame(int gameId, int langId)
        {
            var game = await _context.Games.Include(g => g.Languages)
                        .FirstOrDefaultAsync(g => g.Id == gameId);
            var lang = await _context.Languages.FindAsync(langId);

            if (game == null || lang == null) return false;

            game.Languages.Add(lang);
            return await SaveAsync();
        }

        public async Task<bool> DeleteGame(int id)
        {
            var gameToDelete = await _context.Games.FindAsync(id);
            if (gameToDelete == null) return false;
            _context.Games.Remove(gameToDelete);
            return await SaveAsync();
        }

        public async Task<bool> DeleteGenre(int gameId, int genreId)
        {
            var game = await _context.Games.Include(g => g.Genres)
                        .FirstOrDefaultAsync(g => g.Id == gameId);
            var genre = await _context.Genres.FindAsync(genreId);

            if(game == null || genre == null) { return false; }

            game.Genres.Remove(genre);
            return await SaveAsync();
        }

        public async Task<bool> DeleteSystem(int gameId, int systemId)
        {
            var game = await _context.Games.Include(g => g.CompatibilitySystems)
                        .FirstOrDefaultAsync(g => g.Id == gameId);
            var system = await _context.CompatibilySystems.FindAsync(systemId);

            if (game == null || system == null) { return false; }

            game.CompatibilitySystems.Remove(system);
            return await SaveAsync();
        }

        public async Task<bool> DeleteLang(int gameId, int langId)
        {
            var game = await _context.Games.Include(g => g.Languages)
                        .FirstOrDefaultAsync(g => g.Id == gameId);
            var lang = await _context.Languages.FindAsync(langId);

            if (game == null || lang == null) { return false; }

            game.Languages.Remove(lang);
            return await SaveAsync();
        }

        public async Task<ICollection<Media>> GetMediaByGameId(int id)
        {
            return await _context.Games
                .Where(g => g.Id == id)
                .SelectMany(g => g.Media)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> CheckMediaExists(string url)
        {
            return await _context.Media
            .AnyAsync(m => m.Url.ToLower() == url.ToLower());
        }

        public async Task<bool> AddMedia(int gameId,  Media m)
        {
            var game = await _context.Games.Include(g => g.Media)
            .FirstOrDefaultAsync(g => g.Id == gameId);

            if(game == null) return false;

            game.Media.Add(m);
            return await SaveAsync();
        }

        public async Task<Media?> GetMediaById(int id)
        {
            return await _context.Media.FindAsync(id);
        }

        public async Task<bool> DeleteMedia(Media m)
        {
            _context.Media.Remove(m);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
