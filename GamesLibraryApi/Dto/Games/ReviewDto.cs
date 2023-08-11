using System.ComponentModel.DataAnnotations;

namespace GamesLibraryApi.Dto.Games
{
    public class ReviewDto
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Text { get; set; }
    }
}
