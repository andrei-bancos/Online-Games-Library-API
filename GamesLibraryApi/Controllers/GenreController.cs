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
    public class GenreController : ControllerBase
    {
        private readonly GenreService _service;
        private readonly IMapper _mapper;

        public GenreController(GenreService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/<GenreController>
        [HttpGet]
        [ProducesResponseType(200, Type=typeof(IEnumerable<GenreDto>))]
        public async Task<IActionResult> GetAll()
        {
            var genres = _mapper.Map<List<GenreDto>>(await _service.GetGenres());
            return Ok(genres);
        }

        // GET api/<GenreController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> GetById(int id)
        {
            var genre = _mapper.Map<GenreDto>(await _service.GetById(id));
            if(genre == null) return NotFound();
            return Ok(genre);
        }

        // POST api/<GenreController>/add
        [HttpPost("add/")]
        public async Task<IActionResult> AddNewGenre([FromBody] GenreDto newGenre)
        {
            var checkGenre = await _service.CheckGenreExists(newGenre.Name);

            if(checkGenre)
            {
                ModelState.AddModelError("", "Genre already exists");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid) return BadRequest(ModelState);

            var genreMap = _mapper.Map<Genre>(newGenre);

            if(!await _service.Add(genreMap)) return BadRequest(ModelState);

            return CreatedAtAction(nameof(GetById), new { id = genreMap!.Id }, genreMap);
        }

        // PUT api/<GenreController>/edit/5
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Put(int id, string name)
        {
            var genreToUpdate = await _service.GetById(id);
            if(genreToUpdate == null) return NotFound();

            var checkGenre = await _service.CheckGenreExists(name);

            if(checkGenre)
            {
                ModelState.AddModelError("", "Genre name already exists");
                return StatusCode(422, ModelState);
            }

            if (!await _service.Update(id, name)) return BadRequest(ModelState);
            return Ok("Genre has been changed.");
        }

        // DELETE api/<GenreController>/delete/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _service.GetById(id);
            if (genre == null) return NotFound();
            if(!await _service.Delete(id)) return BadRequest(ModelState);
            return Ok("Genre genre has deleted!");
        }
    }
}
