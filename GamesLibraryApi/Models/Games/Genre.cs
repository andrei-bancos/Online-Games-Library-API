
using System.Text.Json.Serialization;

namespace GamesLibraryApi.Models.Games
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
