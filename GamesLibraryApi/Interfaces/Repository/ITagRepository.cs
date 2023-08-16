using GamesLibraryApi.Models.Games;

namespace GamesLibraryApi.Interfaces.Repository
{
    public interface ITagRepository
    {
        Task<ICollection<Tag>> GetAll();
        Task<Tag?> Get(int id);
        Task<bool> Exists(string name);
        Task<bool> Create(Tag tag);
        Task<bool> Update(int id, string name);
        Task<bool> Delete(int id);
        Task<bool> SaveAsync();
    }
}
