using GamesLibraryApi.Models.Users;

namespace GamesLibraryApi.Models.Games
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set;} = DateTime.Now;

        public int GameId { get; set; }
        public Game Game { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
