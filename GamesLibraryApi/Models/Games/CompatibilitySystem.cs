using System.Text.Json.Serialization;

namespace GamesLibraryApi.Models.Games
{
    public class CompatibilitySystem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Game> Games { get; set;}
    }
}
