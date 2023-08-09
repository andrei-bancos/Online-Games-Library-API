using System.ComponentModel.DataAnnotations;

namespace GamesLibraryApi.Dto.Users
{
    public class ChangePassword
    {
        [Required]
        public string? OldPassword { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter," +
            " one lowercase letter, one digit, and one special character.")]
        public string? NewPassword { get; set; }
        [Required]
        [Compare("NewPassword", 
            ErrorMessage = "The confirmation password does not match")]
        public string? ConfirmNewPassword { get; set; }
    }
}
