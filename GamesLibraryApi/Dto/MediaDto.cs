using System.ComponentModel.DataAnnotations;

namespace GamesLibraryApi.Dto
{
    public class MediaDto
    {
        public int Id { get; set; }
        [RegularExpression("^(image|video)$",
            ErrorMessage = "The value most be 'image' or 'video'")]
        [Required]
        public string? Type { get; set; }
        [Required]
        [Url]
        public string? Url { get; set; }
    }
}
