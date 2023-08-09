using System.ComponentModel.DataAnnotations;

namespace GamesLibraryApi.Dto.Users
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string? email { get; set; }
        [Required]
        public string? password { get; set; }
    }
}
