using System.ComponentModel.DataAnnotations;

namespace GamesLibraryApi.Dto
{
    public class ShowUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
