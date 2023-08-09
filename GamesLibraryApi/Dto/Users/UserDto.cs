using System.ComponentModel.DataAnnotations;

namespace GamesLibraryApi.Dto.Users
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
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter," +
            " one lowercase letter, one digit, and one special character.")]
        public string? Password { get; set; }
        [Required]
        public DateTime? Birthday { get; set; }
        [Required]
        [RegularExpression("^(Male|Female|Other)$",
            ErrorMessage = "Choose 'Male', 'Female' or 'Other'")]
        public string? Gender { get; set; }
    }
}
