using System.ComponentModel.DataAnnotations;

namespace GamesLibraryApi.Dto.Games
{
    public class ShowReviewDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public bool? Recommended { get; set; }
    }
}
