using GamesLibraryApi.Data;
using GamesLibraryApi.Interfaces;
using GamesLibraryApi.Models.Games;
using Microsoft.EntityFrameworkCore;

namespace GamesLibraryApi.Repository
{
    public class TagRepository : ITagRepository
    {
        public readonly DataContext _context;

        public TagRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Tag>> GetTags()
        {
            return await _context.Tags.AsNoTracking().ToListAsync();
        }

        public async Task<Tag?> GetById(int id)
        {
            return await _context.Tags.SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> CheckTagExists(string name)
        {
            return await _context.Tags
            .AnyAsync(t => t.Name.Trim().ToLower() == name.TrimEnd().ToLower());
        }

        public async Task<bool> Add(Tag newTag)
        {
            await _context.Tags.AddAsync(newTag);
            return await SaveAsync();
        }

        public async Task<bool> Update(int id, string name)
        {
            var tagToDelete = await _context.Tags.FindAsync(id);
            if (tagToDelete == null) return false;

            tagToDelete.Name = name;
            return await SaveAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var tagToDelete = await _context.Tags.FindAsync(id);
            if (tagToDelete == null) return false;
            _context.Tags.Remove(tagToDelete);
            return await SaveAsync();
        }


        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
