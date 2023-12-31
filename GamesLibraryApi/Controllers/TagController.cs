﻿using AutoMapper;
using GamesLibraryApi.Models.Games;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GamesLibraryApi.Dto.Games;
using GamesLibraryApi.Interfaces.Services;

namespace GamesLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _service;
        private readonly IMapper _mapper;

        public TagController(ITagService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/<GenreController>
        /// <summary>
        ///     Return all tags
        /// </summary>
        [HttpGet, AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(TagDto))]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _service.GetTags();
            var tagsMap = _mapper.Map<List<TagDto>>(tags);
            return Ok(tagsMap);
        }

        // GET api/<GenreController>/5
        /// <summary>
        ///     Return a tag using tagId
        /// </summary>
        /// <param name="tagId">Id of tag</param>
        [HttpGet("{tagId}"), AllowAnonymous]
        [ProducesResponseType(200, Type=typeof(TagDto))]
        public async Task<IActionResult> GetById(int tagId)
        {
            var tag = await _service.GetById(tagId);
            if(tag == null) return NotFound();

            var tagMap = _mapper.Map<TagDto>(await _service.GetById(tagId));
            return Ok(tagMap);
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

            bool addTag = await _service.Add(newTagMap);
            if (!addTag) return StatusCode(500);

            return CreatedAtAction(nameof(GetById), new { tagId =  newTagMap!.Id }, newTagMap);
        }

        // PUT api/<GenreController>/5
        /// <summary>
        ///     Edit a tag using tagId
        /// </summary>
        /// <param name="tagId">Id of tag</param>
        /// <param name="name">New name</param>
        [HttpPut("{tagId}")]
        public async Task<IActionResult> EditTag(int tagId, string name)
        {
            var tag = await _service.GetById(tagId);
            if (tag == null) return NotFound();

            var checkTag = await _service.CheckTagExists(name);

            if (checkTag)
            {
                ModelState.AddModelError("", "Tag already exists");
                return StatusCode(422, ModelState);
            }

            bool updateTag = await _service.Update(tagId, name);
            if (!updateTag) return StatusCode(500);
            return Ok("Tag has been updated.");
        }

        // DELETE api/<GenreController>/5
        /// <summary>
        ///     Delete a tag using tagId
        /// </summary>
        /// <param name="tagId">Id of tag</param>
        [HttpDelete("{tagId}")]
        public async Task<IActionResult> DeleteTag(int tagId)
        {
            var tag = await _service.GetById(tagId);
            if(tag == null) return NotFound();

            bool deleteTag = await _service.Delete(tagId);
            if (!deleteTag) return StatusCode(500);
            return Ok("Tag has been deleted");
        }
    }
}
