using GamesLibraryApi.Interfaces.Repository;
using GamesLibraryApi.Interfaces.Services;
using GamesLibraryApi.Models.Games;

namespace GamesLibraryApi.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository repository)
        {
            _genreRepository = repository;
        }

        public async Task<ICollection<Genre>> GetGenres()
        {
            return await _genreRepository.GetAll();
        }

        public async Task<Genre?> GetById(int id)
        {
            return await _genreRepository.Get(id);
        }

        public async Task<bool> CheckGenreExists(string name)
        {
            return await _genreRepository.Exists(name);
        }

        public async Task<bool> Add(Genre newGenre)
        {
            return await _genreRepository.Create(newGenre);
        }

        public async Task<bool> Update(int id, string name)
        {
            return await _genreRepository.Update(id, name);
        }

        public async Task<bool> Delete(int id)
        {
            return await _genreRepository.Delete(id);
        }
    }
}
