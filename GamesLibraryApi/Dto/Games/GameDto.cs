using System.ComponentModel.DataAnnotations;

namespace GamesLibraryApi.Dto.Games
{
    public class GameDto
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public bool? IsNew { get; set; }
        [Required]
        public bool? IsFree { get; set; }
        [Required]
        public decimal? Price { get; set; }
        [Required]
        public bool? IsDiscounted { get; set; }
        [Required]
        [Range(0, 100)]
        public int? Discount { get; set; }
        [Required]
        public int? Size { get; set; }
        [Required]
        public bool? PreRelease { get; set; }
        [Required]
        public DateTime? ReleaseDate { get; set; }
        [Required]
        public string? Company { get; set; }
    }
}
