using GamesLibraryApi.Models.Games;

namespace GamesLibraryApi.Models.Users
{
    public class UserGamePurchase
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
    }
}
