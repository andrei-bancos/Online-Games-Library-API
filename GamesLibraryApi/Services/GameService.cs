using GamesLibraryApi.Models.Games;
using GamesLibraryApi.Interfaces.Services;
using GamesLibraryApi.Interfaces.Repository;

namespace GamesLibraryApi.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;
        
        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<Game>> GetGames()
        {
            return await _repository.GetAll();
        }

        public async Task<Game?> GetById(int id)
        {
            return await _repository.Get(id);
        }

        public async Task<bool> CheckGameExists(string name)
        {
            return await _repository.GameExists(name);
        }

        public async Task<bool> Add(Game newGame)
        {
            return await _repository.Add(newGame);
        }

        public async Task<bool> AddGenreToGame(int gameId, int genreId)
        {
            return await _repository.AddGenreToGame(gameId, genreId);
        }

        public async Task<bool> AddTagToGame(int gameId, int tagId)
        {
            return await _repository.AddTagToGame(gameId, tagId);
        }

        public async Task<bool> AddSystemToGame(int gameId, int systemId)
        {
            return await _repository.AddSystemToGame(gameId, systemId);
        }

        public async Task<CompatibilitySystem?> GetSystemById(int id)
        {
            return await _repository.GetSystem(id);
        }

        public async Task<bool> AddLanguageToGame(int gameId, int langId)
        {
            return await _repository.AddLanguageToGame(gameId, langId);
        }

        public async Task<Language?> GetLangById(int id)
        {
            return await _repository.GetLang(id);
        }

        public async Task<bool> DeleteGame(int id)
        {
            return await _repository.DeleteGame(id);
        }

        public async Task<bool> DeleteGenre(int gameId, int genreId)
        {
            return await _repository.DeleteGenre(gameId, genreId);
        }

        public async Task<bool> DeleteTag(int gameId, int tagId)
        {
            return await _repository.DeleteTag(gameId, tagId);
        }

        public async Task<bool> DeleteSystem(int gameId, int systemId)
        {
            return await _repository.DeleteSystem(gameId, systemId);
        }

        public async Task<bool> DeleteLang(int gameId, int langId)
        {
            return await _repository.DeleteLang(gameId, langId);
        }

        public async Task<ICollection<Media>> GetMediaByGameId(int id)
        {
            return await _repository.GetMediaByGameId(id);
        }

        public async Task<bool> CheckMediaExists(string url)
        {
            return await _repository.MediaExists(url);
        }

        public async Task<bool> AddMedia(int gameId,  Media m)
        {
            return await _repository.AddMedia(gameId, m);
        }

        public async Task<Media?> GetMediaById(int id)
        {
            return await _repository.GetMediaById(id);
        }

        public async Task<ICollection<Review>> GetReviewsByGameId(int id)
        {
            return await _repository.GetReviewsByGameId(id);
        }

        public async Task<bool> AddReviewToGame
            (int gameId, Review review, int userId)
        {
            return await _repository.AddReviewToGame(gameId, review, userId);
        }

        public async Task<bool> UpdateGameReview
        (int gameId, int userId, string title, string text, bool recommended)
        {
            return await _repository.UpdateReview(gameId, userId, title, text, recommended);
        }

        public async Task<bool> DeleteGameReview(int gameId, int userId)
        {
            return await _repository.DeleteReview(gameId, userId);
        }

        public async Task<bool> DeleteMedia(Media m)
        {
            return await _repository.DeleteMedia(m);
        }
    }
}
