using GamesLibraryApi.Models.Games;

namespace GamesLibraryApi.Data
{
    public class DbInitializer
    {
        public static void Initializer(DataContext context)
        {
            if (context.Games.Any())
            {
                return;
            }

            var genres = new Genre[] { 
                new Genre { Name = "Action" },
                new Genre { Name = "Adventure" },
                new Genre { Name = "Role-Playing" },
                new Genre { Name = "Strategy" },
                new Genre { Name = "Simulation" }
            };

            var games = new Game[]
            {
                new Game
                {
                    Name = "The Witcher 3: Wild Hunt",
                    Description = "An open-world action RPG.",
                    IsNew = false,
                    IsFree = false,
                    Price = 29.99m,
                    IsDiscounted = true,
                    Discount = 20,
                    Size = 40,
                    PreRelease = false,
                    ReleaseDate = new DateTime(2015, 5, 19),
                    Company = "CD Projekt Red"
                },
                new Game
                {
                    Name = "Grand Theft Auto V",
                    Description = "An open-world action-adventure game.",
                    IsNew = false,
                    IsFree = false,
                    Price = 19.99m,
                    IsDiscounted = true,
                    Discount = 50,
                    Size = 70,
                    PreRelease = false,
                    ReleaseDate = new DateTime(2013, 9, 17),
                    Company = "Rockstar Games"
                },
                new Game
                {
                    Name = "The Elder Scrolls V: Skyrim",
                    Description = "An open-world RPG set in a fantasy world.",
                    IsNew = false,
                    IsFree = false,
                    Price = 14.99m,
                    IsDiscounted = true,
                    Discount = 25,
                    Size = 30,
                    PreRelease = false,
                    ReleaseDate = new DateTime(2011, 11, 11),
                    Company = "Bethesda Game Studios"
                },
                new Game
                {
                    Name = "Dark Souls III",
                    Description = "A challenging action RPG with dark fantasy elements.",
                    IsNew = false,
                    IsFree = false,
                    Price = 39.99m,
                    IsDiscounted = false,
                    Discount = 0,
                    Size = 25,
                    PreRelease = false,
                    ReleaseDate = new DateTime(2016, 4, 12),
                    Company = "FromSoftware"
                },
                new Game
                {
                    Name = "Red Dead Redemption 2",
                    Description = "An open-world western action-adventure game.",
                    IsNew = false,
                    IsFree = false,
                    Price = 49.99m,
                    IsDiscounted = false,
                    Discount = 0,
                    Size = 80,
                    PreRelease = false,
                    ReleaseDate = new DateTime(2018, 10, 26),
                    Company = "Rockstar Games"
                }
            };

            context.Genres.AddRange(genres);
            context.Games.AddRange(games);
            context.SaveChanges();
        }
    }
}
