using GamesLibraryApi.Data;
using GamesLibraryApi.Models.Games;
using Microsoft.EntityFrameworkCore;

namespace GamesLibraryApi.Services
{
    public class GenreService
    {
        private readonly DataContext _context;

        public GenreService(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Genre>> GetGenres()
        {
            return await _context.Genres.AsNoTracking().ToListAsync();
        }

        public Task<Genre?> GetById(int id)
        {
            return _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<bool> CheckGenreExists(string name)
        {
            return await _context.Genres
            .AnyAsync(g => g.Name.Trim().ToLower() == name.TrimEnd().ToLower());
        }

        public async Task<bool> Add(Genre newGenre)
        {
            await _context.Genres.AddAsync(newGenre);
            return await SaveAsync();
        }

        public async Task<bool> Update(int id, string name)
        {
            var genreToUpdate = await _context.Genres.FindAsync(id);
            if (genreToUpdate == null) return false;

            genreToUpdate.Name = name;
            return await SaveAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var genreToDelete = await _context.Genres.FindAsync(id);
            if (genreToDelete == null) return false;
             _context.Remove(genreToDelete);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
