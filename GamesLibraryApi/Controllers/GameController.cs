using AutoMapper;
using GamesLibraryApi.Dto;
using GamesLibraryApi.Models.Games;
using GamesLibraryApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GamesLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository _service;
        private readonly IGenreRepository _serviceGenre;
        private readonly ITagRepository _serviceTag;
        private readonly IMapper _mapper;

        public GameController(IGameRepository service, IMapper mapper, 
            IGenreRepository serviceGenre, ITagRepository serviceTag)
        {
            _service = service;
            _mapper = mapper;
            _serviceGenre = serviceGenre;
            _serviceTag = serviceTag;
        }

        // GET: api/<GameController>
        /// <summary>
        ///     Returns all games with basic information
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GameDto>))]
        public async Task<IActionResult> GetAll()
        {
            var games = await _service.GetGames();
            var gamesMap = _mapper.Map<List<GameDto>>(games);
            return Ok(gamesMap);
        }

        // GET api/<GameController>/5
        /// <summary>
        ///     Return all informations about a game using gameId
        /// </summary>
        [HttpGet("{gameId}")]
        [AllowAnonymous]
        public async Task<ActionResult<Game>> GetById(int gameId)
        {
            var game = await _service.GetById(gameId).ConfigureAwait(false);
            if(game == null) return NotFound();
            return Ok(game);
        }

        // GET api/<GameController>/media/{gameId}
        /// <summary>
        ///     Get all media using a game id
        /// </summary>
        [HttpGet("{gameId}/media/")]
        [AllowAnonymous]
        public async Task<ActionResult> GetMediaByGameId(int gameId)
        {
            var game = await _service.GetById(gameId).ConfigureAwait(false);
            if (game == null) return NotFound();
            ICollection<Media> media = await _service.GetMediaByGameId(gameId);
            var mediaMap = _mapper.Map<ICollection<MediaDto>>(media);
            return Ok(mediaMap);
        }

        // POST api/<GameController>/add
        /// <summary>
        ///     Add a new game
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddNewGame(GameDto newGame)
        {
            bool gameExists = await _service.CheckGameExists(newGame.Name!)
                                            .ConfigureAwait(false);
            if (gameExists)
            {
                ModelState.AddModelError("", "Game already exists");
                return StatusCode(422, ModelState);
            }

            var gameMap = _mapper.Map<Game>(newGame);

            bool addGame = await _service.Add(gameMap);
            if (!addGame) return StatusCode(500);
            return CreatedAtAction(
                nameof(GetById), new { gameId = gameMap!.Id }, gameMap);
        }

        // POST api/<GameController>/{gameId}/genre/{genreId}
        /// <summary>
        ///     Add a genre to a game using gameId and genreId
        /// </summary>
        [HttpPost("{gameId}/genre/{genreId}")]
        public async Task<IActionResult> AddGenreToGame(int gameId, int genreId)
        {
            var game = await _service.GetById(gameId).ConfigureAwait(false);
            if (game == null) return NotFound("Game not found.");

            var genre = await _serviceGenre.GetById(genreId)
                .ConfigureAwait(false);
            if (genre == null) return NotFound("Genre not found.");

            bool genreAlreadyAdded = game.Genres.Any(g => g.Id == genreId);
            if (genreAlreadyAdded) return BadRequest("Genre already added.");

            bool addGenreToGame = await _service
                .AddGenreToGame(gameId, genreId);
            if (!addGenreToGame) return StatusCode(500);
            return Ok("Genre has been added to game.");
        }

        // POST api/<GameController>/{gameId}/tag/{tagId}
        /// <summary>
        ///     Add a tag using gameId and tagId
        /// </summary>
        [HttpPost("{gameId}/tag/{tagId}")]
        public async Task<IActionResult> AddTagToGame(int gameId, int tagId)
        {
            var game = await _service.GetById(gameId).ConfigureAwait(false);
            if (game == null) return NotFound("Game not found.");

            var tag = await _serviceTag.GetById(tagId).ConfigureAwait(false);
            if (tag == null) return NotFound("Tag not found.");

            bool tagAlreadyAdded = game.Tags.Any(t => t.Id == tagId);
            if (tagAlreadyAdded) return BadRequest("Tag already added.");

            bool addTagToGame = await _service.AddTagToGame(gameId, tagId);
            if (!addTagToGame) return StatusCode(500);
            return Ok("Tag has been added to game.");
        }

        // POST api/<GameController>/{gameId}/media
        /// <summary>
        ///     Add media using gameId
        /// </summary>
        [HttpPost("{gameId}/media")]
        public async Task<IActionResult> AddMediaToGame(int gameId, MediaDto mediaDto)
        {
            var game = await _service.GetById(gameId);
            var mediaMap = _mapper.Map<Media>(mediaDto);
            var checkMedia = await _service.CheckMediaExists(mediaMap.Url!);

            if(game == null) return NotFound("Game not found.");

            if (checkMedia)
            {
                ModelState.AddModelError("", "This url already exists");
                return StatusCode(422, ModelState);
            }

            bool addMediaToGame = await _service.AddMedia(gameId, mediaMap);
            if (!addMediaToGame) return StatusCode(500);
            return Ok("Media has been added");
        }

        // POST api/<GameController>/{gameId}/system
        /// <summary>
        ///     Add a compatibility system to a game using gameId and systemId
        /// </summary>
        [HttpPost("{gameId}/system/{systemId}")]
        public async Task<IActionResult> AddSystemToGame(int gameId, int systemId)
        {
            var game = await _service.GetById(gameId).ConfigureAwait(false);
            if (game == null) return NotFound("Game not found.");

            var system = await _service.GetSystemById(systemId);
            if (system == null) return NotFound("System not found.");
            
            bool sysAlreadyAdded = game.CompatibilitySystems
                .Any(sys => sys.Id == systemId);

            if (sysAlreadyAdded) return BadRequest("System already added.");

            bool addSystemToGame = await _service.AddSystemToGame(gameId, systemId);
            if (!addSystemToGame) return StatusCode(500);
            return Ok("System has been added to game");
        }

        // POST api/<GameController>/{gameId}/language
        /// <summary>
        ///     Add a language to a game using gameId and langId
        /// </summary>
        [HttpPost("{gameId}/language/{langId}")]
        public async Task<IActionResult> AddLanguageToGame
            (int gameId, int langId)
        {
            var game = await _service.GetById(gameId).ConfigureAwait(false);
            if (game == null) return NotFound("Game not found.");

            var lang = await _service.GetLangById(langId).ConfigureAwait(false);
            if (lang == null) return NotFound("Language not found.");

            var langAlreadyAdded = game.Languages.Any(l => l.Id == langId);
            if (langAlreadyAdded) return BadRequest("Language already added.");

            bool addLanguageToGame = await _service
                .AddLanguageToGame(gameId, langId);
            if (!addLanguageToGame) return StatusCode(500);
            return Ok("Language has been added to game");
        }

        // DELETE api/<GameController>/5
        /// <summary>
        ///     Delete a game using gameId
        /// </summary>
        [HttpDelete("{gameId}")]
        public async Task<IActionResult> Delete(int gameId)
        {
            var game = await _service.GetById(gameId).ConfigureAwait(false);
            if (game == null) return NotFound();

            bool deleteGame = await _service.DeleteGame(gameId);
            if (!deleteGame) return StatusCode(500);
            return Ok("Game has been deleted");
        }

        // DELETE api/<GameController>/{gameId}/genre/{genreId}
        /// <summary>
        ///     Delete a genre using gameId and genreId
        /// </summary>
        [HttpDelete("{gameId}/genre/{genreId}")]
        public async Task<IActionResult> DeleteGenre(int gameId, int genreId)
        {
            var game = await _service.GetById(gameId).ConfigureAwait(false);
            if (game == null) return NotFound("Game not found.");

            var genre = await _serviceGenre.GetById(genreId)
                .ConfigureAwait(false);
            if (genre == null) return NotFound("Genre not found.");

            var isAdded = game.Genres.Any(g => g.Id == genreId);
            if (!isAdded) return BadRequest("Genre is not added.");

            bool deleteGenre = await _service.DeleteGenre(gameId, genreId);
            if (!deleteGenre) return StatusCode(500);
            return Ok("Genre has been deleted");
        }

        // DELETE api/<GameController>/{gameId}/tag/{genreId}
        /// <summary>
        ///     Delete a genre using gameId and genreId
        /// </summary>
        [HttpDelete("{gameId}/tag/{tagId}")]
        public async Task<IActionResult> DeleteTag(int gameId, int tagId)
        {
            var game = await _service.GetById(gameId).ConfigureAwait(false);
            if (game == null) return NotFound("Game not found.");

            var tag = await _serviceTag.GetById(tagId).ConfigureAwait(false);
            if (tag == null) return NotFound("Tag not found.");

            var isAdded = game.Tags.Any(t => t.Id == tagId);
            if (!isAdded) return BadRequest("Tag is not added.");

            bool deleteTag = await _service.DeleteTag(gameId, tagId);
            if (!deleteTag) return StatusCode(500);
            return Ok("Tag has been deleted");
        }

        // DELETE api/media/{mediaId}
        /// <summary>
        ///     Delete media using mediaId
        /// </summary>
        [HttpDelete("media/{mediaId}")]
        public async Task<IActionResult> DeleteMedia(int mediaId)
        {
            var media = await _service.GetMediaById(mediaId).ConfigureAwait(false);
            if (media == null) return NotFound();

            bool deleteMedia = await _service.DeleteMedia(media);
            if (!deleteMedia) return StatusCode(500);
            return Ok("Media has been deleted");
        }

        // DELETE api/<GameController>/{gameId}/system/{systemId}
        /// <summary>
        ///     Delete a compatibility system using gameId and systemId
        /// </summary>
        [HttpDelete("{gameId}/system/{systemId}")]
        public async Task<IActionResult> DeleteSystem(int gameId, int systemId)
        {
            var game = await _service.GetById(gameId).ConfigureAwait(false);
            if (game == null) return NotFound("Game not found.");

            var sys = await _service.GetSystemById(systemId)
                .ConfigureAwait(false);
            if (sys == null) return NotFound("System not found.");

            var isAdded = game.CompatibilitySystems.Any(s => s.Id == systemId);
            if (!isAdded) return BadRequest("System is not added.");

            bool deleteSystem = await _service.DeleteSystem(gameId, systemId);
            if (!deleteSystem) return StatusCode(500);
            return Ok("System has been deleted");
        }

        // DELETE api/<GameController>/{gameId}/language/{langId}
        /// <summary>
        ///     Delete a language using gameId and langId
        /// </summary>
        [HttpDelete("{gameId}/language/{langId}")]
        public async Task<IActionResult> DeleteLanguage(int gameId, int langId)
        {
            var game = await _service.GetById(gameId).ConfigureAwait(false);
            if (game == null) return NotFound("Game not found.");

            var lang = await _service.GetLangById(langId).ConfigureAwait(false);
            if (lang == null) return NotFound("Language not found.");

            var isAdded = game.Languages.Any(l => l.Id == langId);
            if (!isAdded) return BadRequest("Language is not added.");

            bool deleteLang = await _service.DeleteLang(gameId, langId);
            if (!deleteLang) return StatusCode(500);
            return Ok("Language has been deleted");
        }
    }
}
