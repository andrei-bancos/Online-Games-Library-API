using GamesLibraryApi.Data;
using GamesLibraryApi.Interfaces.Repository;
using GamesLibraryApi.Models.Games;
using Microsoft.EntityFrameworkCore;

namespace GamesLibraryApi.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly DataContext _context;

        public TagRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Tag>> GetAll()
        {
            var tags = await _context.Tags.AsNoTracking().ToListAsync();
            return tags;
        }

        public async Task<Tag?> Get(int id)
        {
            var tag = await _context.Tags
                .SingleOrDefaultAsync(t => t.Id == id);
            return tag;
        }

        public async Task<bool> Exists(string name)
        {
            var exists = await _context.Tags
            .AnyAsync(t => t.Name.Trim().ToLower() == name.TrimEnd().ToLower());
            return exists;
        }

        public async Task<bool> Create(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
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
