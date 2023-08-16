using GamesLibraryApi.Models.Games;

namespace GamesLibraryApi.Interfaces.Services
{
    public interface IGenreService
    {
        Task<ICollection<Genre>> GetGenres();
        Task<Genre?> GetById(int id);
        Task<bool> CheckGenreExists(string name);
        Task<bool> Add(Genre newGenre);
        Task<bool> Update(int id, string name);
        Task<bool> Delete(int id);
    }
}
