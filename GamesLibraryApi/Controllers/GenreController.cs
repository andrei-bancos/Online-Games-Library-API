using AutoMapper;
using GamesLibraryApi.Dto.Games;
using GamesLibraryApi.Interfaces;
using GamesLibraryApi.Models.Games;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GamesLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _service;
        private readonly IMapper _mapper;

        public GenreController(IGenreRepository service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/<GenreController>
        /// <summary>
        ///     Return all genres
        /// </summary>
        [HttpGet, AllowAnonymous]
        [ProducesResponseType(200, Type=typeof(IEnumerable<GenreDto>))]
        public async Task<IActionResult> GetAll()
        {
            var genres = await _service.GetGenres();
            var genresMap = _mapper.Map<List<GenreDto>>(genres);
            return Ok(genresMap);
        }

        // GET api/<GenreController>/5
        /// <summary>
        ///     Return a genre using genreId
        /// </summary>
        [HttpGet("{id}"), AllowAnonymous]
        public async Task<ActionResult<GenreDto>> GetById(int id)
        {
            var genre = await _service.GetById(id);
            if(genre == null) return NotFound();

            var genreMap = _mapper.Map<GenreDto>(genre);
            return Ok(genreMap);
        }

        // POST api/<GenreController>
        /// <summary>
        ///     Add a new genre
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddNewGenre([FromBody] GenreDto newGenre)
        {
            var checkGenre = await _service.CheckGenreExists(newGenre.Name);

            if(checkGenre)
            {
                ModelState.AddModelError("", "Genre already exists");
                return StatusCode(422, ModelState);
            }

            var genreMap = _mapper.Map<Genre>(newGenre);

            bool addGenre = await _service.Add(genreMap);
            if (!addGenre) return StatusCode(500);

            return CreatedAtAction(nameof(GetById), new { id = genreMap!.Id }, genreMap);
        }

        // PUT api/<GenreController>/5
        /// <summary>
        ///     Edit a genre using genreId
        /// </summary>
        [HttpPut("{id}")]
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

            bool genreUpdate = await _service.Update(id, name);
            if (!genreUpdate) return StatusCode(500);
            return Ok("Genre has been changed.");
        }

        // DELETE api/<GenreController>/5
        /// <summary>
        ///     Delete a genre using genreId
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _service.GetById(id);
            if (genre == null) return NotFound();

            bool deleteGenre = await _service.Delete(id);
            if (!deleteGenre) return StatusCode(500);
            return Ok("Genre genre has deleted!");
        }
    }
}
