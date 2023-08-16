using GamesLibraryApi.Data;
using GamesLibraryApi.Interfaces.Repository;
using GamesLibraryApi.Models.Games;
using Microsoft.EntityFrameworkCore;

namespace GamesLibraryApi.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _context;

        public GenreRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<ICollection<Genre>> GetAll()
        {
            var genres = await _context.Genres.AsNoTracking().ToListAsync();
            return genres;
        }

        public async Task<Genre?> Get(int id)
        {
            var genre = await _context.Genres
                                      .SingleOrDefaultAsync(g => g.Id == id);
            return genre;
        }

        public async Task<bool> Exists(string name)
        {
            var exists = await _context.Genres
            .AnyAsync(g => g.Name.Trim().ToLower() == name.TrimEnd().ToLower());
            return exists;
        }

        public async Task<bool> Create(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
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
