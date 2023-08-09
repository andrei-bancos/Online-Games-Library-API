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
        private readonly IMapper _mapper;

        public GameController(IGameRepository service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
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
            return Ok(media);
        }

        // POST api/<GameController>/add
        /// <summary>
        ///     Add a new game
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddNewGame(GameDto newGame)
        {
            bool gameExists = await _service.CheckGameExists(newGame.Name)
                                            .ConfigureAwait(false);
            if (gameExists)
            {
                ModelState.AddModelError("", "Game already exists");
                return StatusCode(422, ModelState);
            }

            var gameMap = _mapper.Map<Game>(newGame);

            if(!await _service.Add(gameMap)) return StatusCode(500);
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
            if(!await _service.AddGenreToGame(gameId, genreId))
                return StatusCode(500);
            return Ok("Genre has been added to game.");
        }

        // POST api/<GameController>/{gameId}/tag/{tagId}
        /// <summary>
        ///     Add a tag using gameId and tagId
        /// </summary>
        [HttpPost("{gameId}/tag/{tagId}")]
        public async Task<IActionResult> AddTagToGame(int gameId, int tagId)
        {
            if (!await _service.AddTagToGame(gameId, tagId))
                return StatusCode(500);
            return Ok("Tag has been added to game.");
        }

        // POST api/<GameController>/{gameId}/media
        /// <summary>
        ///     Add media using gameId
        /// </summary>
        [HttpPost("{gameId}/media")]
        public async Task<IActionResult> AddMediaToGame(int gameId, Media media)
        {
            var checkMedia = await _service.CheckMediaExists(media.Url);

            if (checkMedia)
            {
                ModelState.AddModelError("", "This url already exists");
                return StatusCode(422, ModelState);
            }

            if (!await _service.AddMedia(gameId, media)) 
                return StatusCode(500);
            return Ok("Media has been added");
        }

        // POST api/<GameController>/{gameId}/system
        /// <summary>
        ///     Add a compatibility system to a game using gameId and systemId
        /// </summary>
        [HttpPost("{gameId}/system/{systemId}")]
        public async Task<IActionResult> AddSystemToGame(int gameId, int systemId)
        {
            if (!await _service.AddSystemToGame(gameId, systemId)) 
                return StatusCode(500);
            return Ok("System has been added to game");
        }

        // POST api/<GameController>/{gameId}/language
        /// <summary>
        ///     Add a language to a game using gameId and langId
        /// </summary>
        [HttpPost("{gameId}/language/{langId}")]
        public async Task<IActionResult> AddLanguageToGame(int gameId, int langId)
        {
            if (!await _service.AddLanguageToGame(gameId, langId))
                return StatusCode(500);
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
            if (!await _service.DeleteGame(gameId)) return StatusCode(500);
            return Ok("Game has been deleted");
        }

        // DELETE api/<GameController>/{gameId}/genre/{genreId}
        /// <summary>
        ///     Delete a genre using gameId and genreId
        /// </summary>
        [HttpDelete("{gameId}/genre/{genreId}")]
        public async Task<IActionResult> DeleteGenre(int gameId, int genreId)
        {
            if(!await _service.DeleteGenre(gameId, genreId)) 
                return StatusCode(500);
            return Ok("Genre has been deleted");
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
            if (!await _service.DeleteMedia(media)) return StatusCode(500);
            return Ok("Media has been deleted");
        }

        // DELETE api/<GameController>/{gameId}/system/{systemId}
        /// <summary>
        ///     Delete a compatibility system using gameId and systemId
        /// </summary>
        [HttpDelete("{gameId}/system/{systemId}")]
        public async Task<IActionResult> DeleteSystem(int gameId, int systemId)
        {
            if (!await _service.DeleteSystem(gameId, systemId))
                return StatusCode(500);
            return Ok("System has been deleted");
        }

        // DELETE api/<GameController>/{gameId}/language/{langId}
        /// <summary>
        ///     Delete a language using gameId and langId
        /// </summary>
        [HttpDelete("{gameId}/language/{langId}")]
        public async Task<IActionResult> DeleteLanguage(int gameId, int langId)
        {
            if (!await _service.DeleteLang(gameId, langId))
                return StatusCode(500);
            return Ok("Language has been deleted");
        }
    }
}
