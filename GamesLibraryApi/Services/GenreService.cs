using GamesLibraryApi.Interfaces.Repository;
using GamesLibraryApi.Interfaces.Services;
using GamesLibraryApi.Models.Games;

namespace GamesLibraryApi.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _repository;

        public GenreService(IGenreRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<Genre>> GetGenres()
        {
            return await _repository.GetAll();
        }

        public async Task<Genre?> GetById(int id)
        {
            return await _repository.Get(id);
        }

        public async Task<bool> CheckGenreExists(string name)
        {
            return await _repository.Exists(name);
        }

        public async Task<bool> Add(Genre newGenre)
        {
            return await _repository.Create(newGenre);
        }

        public async Task<bool> Update(int id, string name)
        {
            return await _repository.Update(id, name);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }
    }
}
