using AutoMapper;
using GamesLibraryApi.Dto;
using GamesLibraryApi.Models.Games;
using GamesLibraryApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GamesLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly GameService _service;
        private readonly IMapper _mapper;
        public GameController(GameService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/<GameController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GameDto>))]
        public async Task<IActionResult> GetAll()
        {
            var games = _mapper.Map<List<GameDto>>(await _service.GetGames());
            return Ok(games);
        }

        // GET api/<GameController>/getById/5
        [HttpGet("getById/{id}")]
        public async Task<ActionResult<Game>> GetById(int id)
        {
            var game = await _service.GetById(id);
            
            if(game == null) return NotFound();

            return Ok(game);
        }

        // GET api/<GameController>/media/getByGameId/{gameId}
        [HttpGet("media/getByGameId/{gameId}")]
        public async Task<ActionResult> getMediaByGameId(int gameId)
        {
            var game = await _service.GetById(gameId);
            if (game == null) return NotFound();
            ICollection<Media> media = await _service.GetMediaByGameId(gameId);
            return Ok(media);
        }

        // POST api/<GameController>/add
        [HttpPost("add/")]
        public async Task<IActionResult> AddNewGame(GameDto newGame)
        {
            bool gameExists = await _service.CheckGameExists(newGame.Name);

            if (gameExists)
            {
                ModelState.AddModelError("", "Game already exists");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid) return BadRequest(ModelState);

            var gameMap = _mapper.Map<Game>(newGame);

            if(!await _service.Add(gameMap)) return BadRequest(ModelState);

            return CreatedAtAction(nameof(GetById), new { id = gameMap!.Id }, gameMap);
        }

        // POST api/<GameController>/{gameId}/genre/add/{genreId}
        [HttpPost("{gameId}/genre/add/{genreId}")]
        public async Task<IActionResult> AddGenreToGame(int gameId, int genreId)
        {
            if(!await _service.AddGenreToGame(gameId, genreId))
                return BadRequest(ModelState);
            return Ok("Genre has been added to game.");
        }

        // POST api/<GameController>/{gameId}/tag/add/{tagId}
        [HttpPost("{gameId}/tag/add/{tagId}")]
        public async Task<IActionResult> AddTagToGame(int gameId, int tagId)
        {
            if (!await _service.AddTagToGame(gameId, tagId))
                return BadRequest(ModelState);
            return Ok("Tag has been added to game.");
        }

        // POST api/<GameController>/{gameId}/media/add
        [HttpPost("{gameId}/media/add")]
        public async Task<IActionResult> AddMediaToGame(int gameId, Media m)
        {
            var checkMedia = await _service.CheckMediaExists(m.Url);

            if (checkMedia)
            {
                ModelState.AddModelError("", "This url already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!await _service.AddMedia(gameId, m)) return BadRequest(ModelState);
            return Ok("Media has been added");
        }

        // POST api/<GameController>/{gameId}/system/add
        [HttpPost("{gameId}/system/add/{systemId}")]
        public async Task<IActionResult> AddSystemToGame(int gameId, int systemId)
        {
            if(!await _service.AddSystemToGame(gameId, systemId)) 
                return BadRequest(ModelState);
            return Ok("System has been added to game");
        }

        // POST api/<GameController>/{gameId}/language/add
        [HttpPost("{gameId}/language/add/{langId}")]
        public async Task<IActionResult> AddLanguageToGame(int gameId, int langId)
        {
            if (!await _service.AddLanguageToGame(gameId, langId))
                return BadRequest(ModelState);
            return Ok("Language has been added to game");
        }

        // DELETE api/<GameController>/delete/5
        [HttpDelete("delete/{gameId}")]
        public async Task<IActionResult> Delete(int gameId)
        {
            var game = await _service.GetById(gameId);
            if (game == null) return NotFound();
            if (!await _service.DeleteGame(gameId)) return BadRequest(ModelState);
            return Ok("Game has been deleted");
        }

        // DELETE api/<GameController>/{gameId}/genre/delete/{genreId}
        [HttpDelete("{gameId}/genre/delete/{genreId}")]
        public async Task<IActionResult> DeleteGenre(int gameId, int genreId)
        {
            if(!await _service.DeleteGenre(gameId, genreId)) 
                return BadRequest(ModelState);
            return Ok("Genre has been deleted");
        }

        // DELETE api/delete/media/{mediaId}
        [HttpDelete("delete/media/{mediaId}")]
        public async Task<IActionResult> DeleteMedia(int mediaId)
        {
            var media = await _service.GetMediaById(mediaId);
            if (media == null) return NotFound();
            if (!await _service.DeleteMedia(media)) return BadRequest(ModelState);
            return Ok("Media has been deleted");
        }

        // DELETE api/<GameController>/{gameId}/system/delete/{systemId}
        [HttpDelete("{gameId}/system/delete/{systemId}")]
        public async Task<IActionResult> DeleteSystem(int gameId, int systemId)
        {
            if (!await _service.DeleteSystem(gameId, systemId))
                return BadRequest(ModelState);
            return Ok("System has been deleted");
        }

        // DELETE api/<GameController>/delete/{gameId}/language/{langId}
        [HttpDelete("{gameId}/language/delete/{langId}")]
        public async Task<IActionResult> DeleteLanguage(int gameId, int langId)
        {
            if (!await _service.DeleteLang(gameId, langId))
                return BadRequest(ModelState);
            return Ok("Language has been deleted");
        }
    }
}
