using System.ComponentModel.DataAnnotations;

namespace GamesLibraryApi.Models.Games
{
    public class Media
    {
        public int Id { get; set; }
        [RegularExpression("^(image|video)$", 
            ErrorMessage = "The value most be 'image' or 'video'")]
        [Required]
        public string? type { get; set; }
        [Required]
        [Url]
        public string? Url { get; set; }
    }
}
