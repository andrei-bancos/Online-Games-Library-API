using GamesLibraryApi.Dto;
using GamesLibraryApi.Models.Games;
using GamesLibraryApi.Models.Users;

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
                    Media = new List<Media>
                    {
                        new Media { 
                            Type = "image", 
                            Url = "https://cdn-s-thewitcher.cdprojektred.com/witcher3/backgrounds/comparison-novigrad-after-en@1x.jpg" 
                        },
                        new Media
                        {
                            Type = "video",
                            Url = "https://www.youtube.com/watch?v=nBT2SP21f3Q"
                        }
                    },
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
                    Media = new List<Media>
                    {
                        new Media {
                            Type = "image",
                            Url = "https://media-rockstargames-com.akamaized.net/tina-uploads/tina-modules/6937/4ff07bc5a78d5de03deb1bd10cdc94ea0ab1a4eb.png"
                        },
                        new Media
                        {
                            Type = "video",
                            Url = "https://videos-rockstargames-com.akamaized.net/v3/V-4193e6444906a81da7fc1a4e85eb99c290dc4b2681ef4ffa55-en_us-1080p.mp4"
                        }
                    },
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
                    Media = new List<Media>
                    {
                        new Media {
                            Type = "image",
                            Url = "https://images.ctfassets.net/rporu91m20dc/4i01HtT8mIECcSsAo2QqiK/e61e0111cfe919d66949877994d4661d/skyrim-overview-media--riverwood.jpg?q=70&&&fm=webp"
                        },
                        new Media
                        {
                            Type = "video",
                            Url = "https://www.youtube.com/watch?v=QT1httL_ZNc"
                        }
                    },
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
                    Media = new List<Media>
                    {
                        new Media {
                            Type = "image",
                            Url = "https://p325k7wa.twic.pics/high/dark-souls/dark-souls-3/00-page-setup/ds3_banner.jpg?twic=v1/resize=1900/step=10/quality=80"
                        },
                        new Media
                        {
                            Type = "video",
                            Url = "https://www.youtube.com/watch?v=-NHG1-wlUh4"
                        }
                    },
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
                    Media = new List<Media>
                    {
                        new Media {
                            Type = "image",
                            Url = "https://www.rockstargames.com/reddeadredemption2/dist/img/en_us/home/f4cb1c5406470fdb7765ff50a604b84a.jpg"
                        },
                        new Media
                        {
                            Type = "video",
                            Url = "https://videos-rockstargames-com.akamaized.net/v3/reddeadredemption2-halloweenpass2-en_us-1080p.mp4"
                        }
                    },
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

            var admin = new User
            {
                Username = "admin",
                Email = "admin@test.com",
                Password = BCrypt.Net.BCrypt.HashPassword("Admin123"),
                Role = "Admin",
                Birthday = DateTime.Now,
                Gender = "Male",
                CreatedAt = DateTime.Now
            };

            var user = new User
            {
                Username = "user",
                Email = "user@test.com",
                Password = BCrypt.Net.BCrypt.HashPassword("User123"),
                Role = "User",
                Birthday = DateTime.Now,
                Gender = "Male",
                CreatedAt = DateTime.Now
            };

            var ugp = new UserGamePurchase[]
            {
                new UserGamePurchase {
                    UserId = 1,
                    GameId = 2,
                    PurchasePrice = games[1].Price,
                    PurchaseDate = DateTime.Now
                },
                new UserGamePurchase {
                    UserId = 1,
                    GameId = 5,
                    PurchasePrice = games[4].Price,
                    PurchaseDate = DateTime.Now
                },
                new UserGamePurchase {
                    UserId = 2,
                    GameId = 1,
                    PurchasePrice = games[0].Price,
                    PurchaseDate = DateTime.Now
                },
                new UserGamePurchase {
                    UserId = 2,
                    GameId = 3,
                    PurchasePrice = games[2].Price,
                    PurchaseDate = DateTime.Now
                }
            };

            var reviews = new Review[]
            {
                new Review {
                    GameId = 2,
                    UserId = 1,
                    Title = "Living the High-Speed Dream!",
                    Text = "GTA V has me living life in the fast lane! Cruis" +
                    "ing through Los Santos feels crazy real, and those heis" +
                    "ts? Heart-pounding excitement! Characters are like old " +
                    "pals, and the attention to detail is unreal. Only downs" +
                    "ide? Sometimes missions felt repetitive. But hey, it's " +
                    "still my top pick for open-world fun.",
                    Recommended = true
                },
                new Review { 
                    GameId = 5, 
                    UserId = 1, 
                    Title = "Saddling Up for Adventure!", 
                    Text = "Red Dead Redemption 2 is a wild west dream. Gall" +
                    "oping across open plains? Check. Gunslinging showdowns?" +
                    " Check. The world feels alive, but controls can be a bi" +
                    "t wonky. Side quests kept me busy, maybe too busy. But " +
                    "overall? Wrangled myself an epic time!", 
                    Recommended = true 
                },
                new Review {
                    GameId = 1,
                    UserId = 2,
                    Title = "Witcher",
                    Text = "Witcher 3's fantasy world is insane! Magic, mons" +
                    "ters, and quests that pull you in. Combat's a bit clunk" +
                    "y, took me a while to get used to. Quests, on the other" +
                    " hand? Almost too much to handle. But man, immersing my" +
                    "self in that world was worth it!",
                    Recommended = false
                },
                new Review {
                    GameId = 3,
                    UserId = 2,
                    Title = "Skyrim - My Fantasy Playground!",
                    Text = "Skyrim's like a fantasy playground! Exploring du" +
                    "ngeons, fighting dragons – it's the stuff of legends. W" +
                    "orld's massive, but sometimes things get glitchy. Quest" +
                    "s? Endless. But hey, that's what I signed up for. Lost " +
                    "track of time in this epic world.",
                    Recommended = true
                }
            };

            context.Genres.AddRange(genres);
            context.Tags.AddRange(tags);
            context.CompatibilySystems.AddRange(systems);
            context.Languages.AddRange(languages);
            context.Games.AddRange(games);
            context.Users.Add(admin);
            context.Users.Add(user);
            context.UserGamePurchases.AddRange(ugp);
            context.Reviews.AddRange(reviews);
            context.SaveChanges();
        }
    }
}
