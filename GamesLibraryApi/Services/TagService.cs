using GamesLibraryApi.Interfaces.Repository;
using GamesLibraryApi.Interfaces.Services;
using GamesLibraryApi.Models.Games;

namespace GamesLibraryApi.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;

        public TagService(ITagRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<Tag>> GetTags()
        {
            return await _repository.GetAll();
        }

        public async Task<Tag?> GetById(int id)
        {
            return await _repository.Get(id);
        }

        public async Task<bool> CheckTagExists(string name)
        {
            return await _repository.Exists(name);
        }

        public async Task<bool> Add(Tag newTag)
        {
            return await _repository.Create(newTag);
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
