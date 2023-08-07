using GamesLibraryApi.Data;
using GamesLibraryApi.Interfaces;
using GamesLibraryApi.Models.Games;
using Microsoft.EntityFrameworkCore;

namespace GamesLibraryApi.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _context;

        public GenreRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Genre>> GetGenres()
        {
            return await _context.Genres.AsNoTracking().ToListAsync();
        }

        public async Task<Genre?> GetById(int id)
        {
            return await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
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
