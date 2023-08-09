namespace GamesLibraryApi.Models.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "User";
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<UserGamePurchase> UserGamePurchases { get; set; }
    }
}
