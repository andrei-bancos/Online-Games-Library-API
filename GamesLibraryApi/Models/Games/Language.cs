using System.Text.Json.Serialization;

namespace GamesLibraryApi.Models.Games
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        [JsonIgnore]
        public ICollection<Game> Games { get; set; }
    }
}
