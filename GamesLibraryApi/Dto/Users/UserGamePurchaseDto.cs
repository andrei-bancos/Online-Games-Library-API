namespace GamesLibraryApi.Dto.Users
{
    public class UserGamePurchaseDto
    {
        public int GameId { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
    }
}
