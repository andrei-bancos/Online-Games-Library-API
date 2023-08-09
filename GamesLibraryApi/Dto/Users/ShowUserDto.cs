﻿namespace GamesLibraryApi.Dto.Users
{
    public class ShowUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public string Gender { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public ICollection<UserGamePurchaseDto> GamePurchases { get; set; }
    }
}
