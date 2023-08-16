using GamesLibraryApi.Models.Games;

namespace GamesLibraryApi.Interfaces.Repository
{
    public interface IGenreRepository
    {
        Task<ICollection<Genre>> GetAll();
        Task<Genre?> Get(int id);
        Task<bool> Exists(string name);
        Task<bool> Create(Genre genre);
        Task<bool> Update(int id, string name);
        Task<bool> Delete(int id);
        Task<bool> SaveAsync();
    }
}
