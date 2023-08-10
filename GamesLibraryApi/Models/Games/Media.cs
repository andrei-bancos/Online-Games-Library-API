using System.ComponentModel.DataAnnotations;

namespace GamesLibraryApi.Models.Games
{
    public class Media
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Url { get; set; }
    }
}
