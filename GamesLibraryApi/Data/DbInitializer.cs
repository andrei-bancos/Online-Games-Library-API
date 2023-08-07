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

            var tags = new Tag[]
            {
                new Tag { Name = "Open World" },
                new Tag { Name = "RPG Elements" },
                new Tag { Name = "Multiplayer" },
                new Tag { Name = "Puzzle Challenges" },
                new Tag { Name = "Fast-Paced Action" }
            };

            var systems = new CompatibilitySystem[]
            {
                new CompatibilitySystem { Name = "Windows" },
                new CompatibilitySystem { Name = "Linux" },
                new CompatibilitySystem { Name = "macOS" }
            };

            var languages = new Language[]
{
                new Language { Name = "English", Code = "en" },
                new Language { Name = "Spanish", Code = "es" },
                new Language { Name = "French", Code = "fr" },
                new Language { Name = "German", Code = "de" },
                new Language { Name = "Japanese", Code = "ja" },
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
                    Company = "CD Projekt Red",
                    Genres = new List<Genre> { genres[0], genres[3] },
                    Tags = new List<Tag> { tags[1], tags[3] },
                    CompatibilitySystems =
                    new List<CompatibilitySystem> { systems[0], systems[1] },
                    Languages = new List<Language> { 
                        languages[0], 
                        languages[1], 
                        languages[2],
                        languages[3], 
                        languages[4]
                    }
                    
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
                    Company = "Rockstar Games",
                    Genres = new List<Genre> { genres[1], genres[2] },
                    Tags = new List<Tag> { tags[1], tags[4] },
                    CompatibilitySystems =
                    new List<CompatibilitySystem> { systems[0], systems[1] },
                    Languages = new List<Language> {
                        languages[0],
                        languages[1],
                        languages[2],
                        languages[3],
                        languages[4]
                    }
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
                    Company = "Bethesda Game Studios",
                    Genres = new List<Genre> { genres[2], genres[4] },
                    Tags = new List<Tag> { tags[2], tags[4] },
                    CompatibilitySystems =
                    new List<CompatibilitySystem> { systems[0], systems[1] },
                    Languages = new List<Language> {
                        languages[0],
                        languages[1],
                        languages[2],
                        languages[3],
                        languages[4]
                    }
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
                    Company = "FromSoftware",
                    Genres = new List<Genre> { genres[0], genres[1] },
                    Tags = new List<Tag> { tags[2], tags[3] },
                    CompatibilitySystems =
                    new List<CompatibilitySystem> { systems[0], systems[1] },
                    Languages = new List<Language> {
                        languages[0],
                        languages[1],
                        languages[2],
                        languages[3],
                        languages[4]
                    }
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
                    Company = "Rockstar Games",
                    Genres = new List<Genre> { genres[0], genres[1] },
                    Tags = new List<Tag> { tags[0], tags[1] },
                    CompatibilitySystems =
                    new List<CompatibilitySystem> { systems[0], systems[1] },
                    Languages = new List<Language> {
                        languages[0],
                        languages[1],
                        languages[2],
                        languages[3],
                        languages[4]
                    }
                }
            };

            context.Genres.AddRange(genres);
            context.Tags.AddRange(tags);
            context.CompatibilySystems.AddRange(systems);
            context.Languages.AddRange(languages);
            context.Games.AddRange(games);
            context.SaveChanges();
        }
    }
}
