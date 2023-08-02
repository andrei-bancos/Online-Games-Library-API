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
        GenreService _service;
        private readonly IMapper _mapper;

        public GenreController(GenreService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/<GenreController>
        [HttpGet]
        [ProducesResponseType(200, Type=typeof(IEnumerable<GenreDto>))]
        public IActionResult GetAll()
        {
            var allGenre = _mapper.Map<List<GenreDto>>(_service.GetGenres());
            return Ok(allGenre);
        }

        // GET api/<GenreController>/5
        [HttpGet("{id}")]
        public ActionResult<GenreDto> GetById(int id)
        {
            var genre = _mapper.Map<GenreDto>(_service.GetById(id));
            if(genre == null) return NotFound();
            return Ok(genre);
        }

        // POST api/<GenreController>/addGenre
        [HttpPost("addGenre/")]
        public IActionResult AddNewGenre([FromBody] GenreDto newGenre)
        {
            var checkGenre = _service.GetGenres()
                .Where(g => g.Name.Trim().ToLower() == newGenre.Name.TrimEnd().ToLower())
                .FirstOrDefault();

            if(checkGenre != null)
            {
                ModelState.AddModelError("", "Genre already exists");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid) return BadRequest(ModelState);

            var genreMap = _mapper.Map<Genre>(newGenre);

            if(!_service.Add(genreMap)) return BadRequest(ModelState);

            return CreatedAtAction(nameof(GetById), new { id = genreMap!.Id }, genreMap);
        }

        // PUT api/<GenreController>/edit/5
        [HttpPut("edit/{id}")]
        public IActionResult Put(int id, string name)
        {
            var genreToUpdate = _service.GetById(id);
            if(genreToUpdate == null) return NotFound();

            var checkIfNameExists = _service.GetGenres()
                .Where(g => g.Name.Trim().ToLower() == name.TrimEnd().ToLower())
                .FirstOrDefault();

            if(checkIfNameExists != null)
            {
                ModelState.AddModelError("", "Genre name already exists");
                return StatusCode(422, ModelState);
            }

            if (!_service.Update(id, name)) return BadRequest(ModelState);
            return Ok("Genre has been changed.");
        }

        // DELETE api/<GenreController>/delete/5
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var genre = _service.GetById(id);
            if (genre == null) return NotFound();
            if(!_service.Delete(id)) return BadRequest(ModelState);
            return Ok("Genre genre has deleted!");
        }
    }
}
