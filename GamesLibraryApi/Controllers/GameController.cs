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
        GameService _service;
        private readonly IMapper _mapper;
        public GameController(GameService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/<GameController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GameDto>))]
        public IActionResult GetAll()
        {
            var allGames = _mapper.Map<List<GameDto>>(_service.GetGames());
            return Ok(allGames);
        }

        // GET api/<GameController>/getById/5
        [HttpGet("getById/{id}")]
        public ActionResult<Game> GetById(int id)
        {
            var game = _service.GetById(id);
            
            if(game == null) return NotFound();

            return Ok(game);
        }

        // POST api/<GameController>/addGame
        [HttpPost("addGame/")]
        public IActionResult AddNewGame(GameDto newGame)
        {
            var checkGame = _service.GetGames()
                .Where(g => g.Name.Trim().ToLower() == newGame.Name.TrimEnd().ToLower())
                .FirstOrDefault();

            if(checkGame != null)
            {
                ModelState.AddModelError("", "Game already exists");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid) return BadRequest(ModelState);

            var gameMap = _mapper.Map<Game>(newGame);

            if(!_service.Add(gameMap)) return BadRequest(ModelState);

            return CreatedAtAction(nameof(GetById), new { id = gameMap!.Id }, gameMap);
        }

        [HttpPost("{gameId}/genre/addGenre/{genreId}")]
        public IActionResult AddGenreToGame(int gameId, int genreId)
        {
            if(!_service.AddGenreToGame(gameId, genreId))
                return BadRequest(ModelState);
            return Ok("Genre has been added to game.");
        }

        // DELETE api/<GameController>/delete/5
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var game = _service.GetById(id);
            if (game == null) return NotFound();
            if (!_service.Delete(id)) return BadRequest(ModelState);
            return Ok("Game has been deleted.");
        }

        [HttpDelete("delete/{gameId}/genre/{genreId}")]
        public IActionResult DeleteGenre(int gameId, int genreId)
        {
            if(!_service.DeleteGenre(gameId, genreId)) 
                return BadRequest(ModelState);
            return Ok();
        }
    }
}
