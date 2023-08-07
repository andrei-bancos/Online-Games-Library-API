using AutoMapper;
using GamesLibraryApi.Dto;
using GamesLibraryApi.Models.Games;
using GamesLibraryApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GamesLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _service;
        private readonly IMapper _mapper;

        public TagController(ITagRepository service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/<GenreController>
        /// <summary>
        ///     Return all tags
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(TagDto))]
        public async Task<IActionResult> GetAll()
        {
            var tags = _mapper.Map<List<TagDto>>(await _service.GetTags());
            return Ok(tags);
        }

        // GET api/<GenreController>/5
        /// <summary>
        ///     Return a tag using tagId
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type=typeof(TagDto))]
        public async Task<IActionResult> GetById(int id)
        {
            var tag = _mapper.Map<TagDto>(await _service.GetById(id));
            if(tag == null) return NotFound();
            return Ok(tag);
        }

        // POST api/<GenreController>
        /// <summary>
        ///     Add a new tag
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddNewTag(TagDto newTag)
        {
            var checkTag = await _service.CheckTagExists(newTag.Name);

            if(checkTag)
            {
                ModelState.AddModelError("", "Tag already exists");
                return StatusCode(422, ModelState);
            }

            var newTagMap = _mapper.Map<Tag>(newTag);

            if (!await _service.Add(newTagMap)) return StatusCode(500);

            return CreatedAtAction(nameof(GetById), new { id =  newTagMap!.Id }, newTagMap);
        }

        // PUT api/<GenreController>/5
        /// <summary>
        ///     Edit a tag using tagId
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTag(int id, string name)
        {
            var tag = await _service.GetById(id);
            if (tag == null) return NotFound();

            var checkTag = await _service.CheckTagExists(name);

            if (checkTag)
            {
                ModelState.AddModelError("", "Tag already exists");
                return StatusCode(422, ModelState);
            }

            if(!await _service.Update(id, name)) return StatusCode(500);
            return Ok("Tag has been updated.");
        }

        // DELETE api/<GenreController>/5
        /// <summary>
        ///     Delete a tag using tagId
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var tag = await _service.GetById(id);
            if(tag == null) return NotFound();
            if(!await _service.Delete(id)) return StatusCode(500);
            return Ok("Tag has been deleted");
        }
    }
}
