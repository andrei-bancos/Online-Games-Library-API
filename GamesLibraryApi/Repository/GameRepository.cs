﻿using GamesLibraryApi.Data;
using GamesLibraryApi.Interfaces.Repository;
using GamesLibraryApi.Models.Games;
using Microsoft.EntityFrameworkCore;

namespace GamesLibraryApi.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly DataContext _context;

        public GameRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Game>> GetAll()
        {
            return await _context.Games.AsNoTracking().ToListAsync();
        }

        public async Task<Game?> Get(int id)
        {
            return await _context.Games
                .Include(g => g.Genres)
                .Include(g => g.Tags)
                .Include(g => g.Media)
                .Include(g => g.CompatibilitySystems)
                .Include(g => g.Languages)
                .Include(g => g.Reviews)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<bool> GameExists(string name)
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

            if (game == null || genre == null) return false;

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

        public async Task<CompatibilitySystem?> GetSystem(int id)
        {
            var system = await _context.CompatibilySystems.FindAsync(id);
            return system;
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

        public async Task<Language?> GetLang(int id)
        {
            var lang = await _context.Languages.FindAsync(id);
            return lang;
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

            if (game == null || genre == null) return false;

            game.Genres.Remove(genre);
            return await SaveAsync();
        }

        public async Task<bool> DeleteTag(int gameId, int tagId)
        {
            var game = await _context.Games.Include(g => g.Tags)
                        .FirstOrDefaultAsync(g => g.Id == gameId);
            var tag = await _context.Tags.FindAsync(tagId);

            if (game == null || tag == null) return false;

            game.Tags.Remove(tag);
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

        public async Task<bool> MediaExists(string url)
        {
            return await _context.Media
            .AnyAsync(m => m.Url.ToLower() == url.ToLower());
        }

        public async Task<bool> AddMedia(int gameId, Media m)
        {
            var game = await _context.Games.Include(g => g.Media)
            .FirstOrDefaultAsync(g => g.Id == gameId);

            if (game == null) return false;

            game.Media.Add(m);
            return await SaveAsync();
        }

        public async Task<Media?> GetMediaById(int id)
        {
            return await _context.Media.FindAsync(id);
        }

        public async Task<ICollection<Review>> GetReviewsByGameId(int id)
        {
            var reviews = await _context.Games
                                        .Where(g => g.Id == id)
                                        .SelectMany(g => g.Reviews)
                                        .AsNoTracking().ToListAsync();
            return reviews;
        }

        public async Task<bool> AddReviewToGame
            (int gameId, Review review, int userId)
        {
            var game = await _context.Games.FindAsync(gameId);
            var user = await _context.Users.FindAsync(userId);

            if (game == null || user == null) return false;

            review.UserId = userId;
            review.GameId = gameId;

            _context.Reviews.Add(review);

            return await SaveAsync();
        }

        public async Task<bool> UpdateReview
        (int gameId, int userId, string title, string text, bool recommended)
        {
            var review = await _context.Reviews
                .FirstOrDefaultAsync(
                r => r.GameId == gameId && r.UserId == userId
                );
            if (review == null) return false;

            review.Title = title;
            review.Text = text;
            review.Recommended = recommended;

            return await SaveAsync();
        }

        public async Task<bool> DeleteReview(int gameId, int userId)
        {
            var review = await _context.Reviews
                .FirstOrDefaultAsync(
                r => r.GameId == gameId && r.UserId == userId
                );
            if (review == null) return false;
            _context.Reviews.Remove(review);
            return await SaveAsync();
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
