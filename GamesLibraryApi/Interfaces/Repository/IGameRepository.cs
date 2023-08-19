using GamesLibraryApi.Models.Games;

namespace GamesLibraryApi.Interfaces.Repository
{
    public interface IGameRepository
    {
        Task<ICollection<Game>> GetAll();
        Task<Game?> Get(int id);
        Task<bool> GameExists(string name);
        Task<bool> Add(Game newGame);
        Task<bool> AddGenreToGame(int gameId, int genreId);
        Task<bool> AddTagToGame(int gameId, int tagId);
        Task<bool> AddSystemToGame(int gameId, int systemId);
        Task<CompatibilitySystem?> GetSystem(int id);
        Task<bool> AddLanguageToGame(int gameId, int langId);
        Task<Language?> GetLang(int id);
        Task<bool> DeleteGame(int id);
        Task<bool> DeleteGenre(int gameId, int genreId);
        Task<bool> DeleteTag(int gameId, int tagId);
        Task<bool> DeleteSystem(int gameId, int systemId);
        Task<bool> DeleteLang(int gameId, int langId);
        Task<ICollection<Media>> GetMediaByGameId(int id);
        Task<bool> MediaExists(string url);
        Task<bool> AddMedia(int gameId, Media m);
        Task<Media?> GetMediaById(int id);
        Task<ICollection<Review>> GetReviewsByGameId(int id);
        Task<bool> AddReviewToGame
            (int gameId, Review review, int userId);
        Task<bool> UpdateReview
        (int gameId, int userId, string title, string text, bool recommended);
        Task<bool> DeleteReview(int gameId, int userId);
        Task<bool> DeleteMedia(Media m);
        Task<bool> SaveAsync();
    }
}
