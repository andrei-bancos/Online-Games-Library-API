using GamesLibraryApi.Models.Games;

namespace GamesLibraryApi.Interfaces
{
    public interface IGameRepository
    {
        Task<ICollection<Game>> GetGames();
        Task<Game?> GetById(int id);
        Task<bool> CheckGameExists(string name);
        Task<bool> Add(Game newGame);
        Task<bool> AddGenreToGame(int gameId, int genreId);
        Task<bool> AddTagToGame(int gameId, int tagId);
        Task<bool> AddSystemToGame(int gameId, int systemId);
        Task<CompatibilitySystem?> GetSystemById(int id);
        Task<bool> AddLanguageToGame(int gameId, int langId);
        Task<Language?> GetLangById(int id);
        Task<bool> DeleteGame(int id);
        Task<bool> DeleteGenre(int gameId, int genreId);
        Task<bool> DeleteTag(int gameId, int tagId);
        Task<bool> DeleteSystem(int gameId, int systemId);
        Task<bool> DeleteLang(int gameId, int langId);
        Task<ICollection<Media>> GetMediaByGameId(int id);
        Task<bool> CheckMediaExists(string url);
        Task<bool> AddMedia(int gameId, Media m);
        Task<Media?> GetMediaById(int id);
        Task<bool> DeleteMedia(Media m);
        Task<bool> SaveAsync();
    }
}
