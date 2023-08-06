using GamesLibraryApi.Models.Games;
using Microsoft.EntityFrameworkCore;

namespace GamesLibraryApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // Games tables
        public DbSet<Game> Games => Set<Game>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<Media> Media => Set<Media>();
        public DbSet<CompatibilySystem> CompatibilySystems 
            => Set<CompatibilySystem>();
        public DbSet<Language> Languages => Set<Language>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // many to many Game <-> Genre
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Genres)
                .WithMany(g => g.Games)
                .UsingEntity("GameGenre");

            // many to many Game <-> Tag
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Tags)
                .WithMany(g => g.Games)
                .UsingEntity("GameTag");

            // many to many Game <-> CompatibilySystem
            modelBuilder.Entity<Game>()
                .HasMany(g => g.compatibilySystems)
                .WithMany(g => g.Games)
                .UsingEntity("GameOS");

            // many to many Game <-> Language
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Languages)
                .WithMany(g => g.Games)
                .UsingEntity("GameLang");
        }
    }
}
