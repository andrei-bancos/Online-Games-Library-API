using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamesLibraryApi.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
