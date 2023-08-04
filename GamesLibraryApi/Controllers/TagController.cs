using AutoMapper;
using GamesLibraryApi.Dto;
using GamesLibraryApi.Models.Games;
using GamesLibraryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamesLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly TagService _service;
        private readonly IMapper _mapper;

        public TagController(TagService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(TagDto))]
        public async Task<IActionResult> GetAll()
        {
            var tags = _mapper.Map<List<TagDto>>(await _service.GetTags());
            return Ok(tags);
        }

        [HttpGet("getById/{id}")]
        [ProducesResponseType(200, Type=typeof(TagDto))]
        public async Task<IActionResult> GetById(int id)
        {
            var tag = _mapper.Map<TagDto>(await _service.GetById(id));
            if(tag == null) return NotFound();
            return Ok(tag);
        }

        [HttpPost("addTag/")]
        public async Task<IActionResult> AddNewTag(TagDto newTag)
        {
            var checkTag = await _service.CheckTagExists(newTag.Name);

            if(checkTag)
            {
                ModelState.AddModelError("", "Tag already exists");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid) return BadRequest(ModelState);

            var newTagMap = _mapper.Map<Tag>(newTag);

            if (!await _service.Add(newTagMap)) return BadRequest(ModelState);

            return CreatedAtAction(nameof(GetById), new { id =  newTagMap!.Id }, newTagMap);
        }

        [HttpPut("edit/{id}")]
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

            if(!await _service.Update(id, name)) return BadRequest(ModelState);
            return Ok("Tag has been updated.");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var tag = await _service.GetById(id);
            if(tag == null) return NotFound();
            if(!await _service.Delete(id)) return BadRequest(ModelState);
            return Ok("Tag has been deleted");
        }
    }
}
