using GamesLibraryApi.Interfaces.Repository;
using GamesLibraryApi.Interfaces.Services;
using GamesLibraryApi.Models.Games;

namespace GamesLibraryApi.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository tagRepository;

        public TagService(ITagRepository repository)
        {
            tagRepository = repository;
        }

        public async Task<ICollection<Tag>> GetTags()
        {
            return await tagRepository.GetAll();
        }

        public async Task<Tag?> GetById(int id)
        {
            return await tagRepository.Get(id);
        }

        public async Task<bool> CheckTagExists(string name)
        {
            return await tagRepository.Exists(name);
        }

        public async Task<bool> Add(Tag newTag)
        {
            return await tagRepository.Create(newTag);
        }

        public async Task<bool> Update(int id, string name)
        {
            return await tagRepository.Update(id, name);
        }

        public async Task<bool> Delete(int id)
        {
            return await tagRepository.Delete(id);
        }
    }
}
