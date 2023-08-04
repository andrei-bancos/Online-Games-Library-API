namespace GamesLibraryApi.Models.Games
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsNew { get; set; }
        public bool IsFree { get; set; }
        public decimal Price { get; set; }
        public bool IsDiscounted { get; set; }
        public int Discount { get; set; }
        public int Size { get; set; }
        public bool PreRelease { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Company { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Media> Media { get; set; }
        public ICollection<CompatibilySystem> compatibilySystems { get; set; }
        public ICollection<Language> Languages { get; set; }
    }
}
