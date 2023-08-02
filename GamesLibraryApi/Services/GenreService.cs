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

        public ICollection<Genre> GetGenres()
        {
            return _context.Genres.AsNoTracking().ToList();
        }

        public Genre? GetById(int id)
        {
            return _context.Genres.SingleOrDefault(g => g.Id == id);
        }

        public bool Add(Genre newGenre)
        {
            _context.Genres.Add(newGenre);
            return Save();
        }

        public bool Update(int id, string name)
        {
            var genreToUpdate = _context.Genres.Find(id);
            if (genreToUpdate == null) return false;

            genreToUpdate.Name = name;
            return Save();
        }

        public bool Delete(int id)
        {
            var genreToDelete = _context.Genres.Find(id);
            if (genreToDelete == null) return false;
             _context.Remove(genreToDelete);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
