using GamesLibraryApi.Models.Games;
using GamesLibraryApi.Models.Users;
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
        public DbSet<CompatibilitySystem> CompatibilySystems 
            => Set<CompatibilitySystem>();
        public DbSet<Language> Languages => Set<Language>();
        public DbSet<User> Users => Set<User>();
        public DbSet<UserGamePurchase> UserGamePurchases 
            => Set<UserGamePurchase>();
        public DbSet<Review> Reviews => Set<Review>();


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
                .HasMany(g => g.CompatibilitySystems)
                .WithMany(g => g.Games)
                .UsingEntity("GameOS");

            // many to many Game <-> Language
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Languages)
                .WithMany(g => g.Games)
                .UsingEntity("GameLang");

            // many to many Game <-> Ugp <-> User
            modelBuilder.Entity<UserGamePurchase>()
                .HasKey(ugp => new { ugp.UserId, ugp.GameId });

            modelBuilder.Entity<UserGamePurchase>()
                .HasOne(ugp => ugp.User)
                .WithMany(u => u.UserGamePurchases)
                .HasForeignKey(ugp => ugp.UserId);

            modelBuilder.Entity<UserGamePurchase>()
                .HasOne(ugp => ugp.Game)
                .WithMany(g => g.UserGamePurchases)
                .HasForeignKey(ugp => ugp.GameId);

            // one to many Game <-> Review
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Reviews)
                .WithOne(g => g.Game)
                .HasForeignKey(g => g.GameId);

            // one to many User <-> Review
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reviews)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);
        }
    }
}
