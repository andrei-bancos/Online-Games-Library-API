using GamesLibraryApi.Models.Games;

namespace GamesLibraryApi.Interfaces
{
    public interface ITagRepository
    {
        Task<ICollection<Tag>> GetTags();
        Task<Tag?> GetById(int id);
        Task<bool> CheckTagExists(string name);
        Task<bool> Add(Tag newTag);
        Task<bool> Update(int id, string name);
        Task<bool> Delete(int id);
        Task<bool> SaveAsync();
    }
}
