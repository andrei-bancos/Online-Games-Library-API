using AutoMapper;
using GamesLibraryApi.Dto;
using GamesLibraryApi.Interfaces;
using GamesLibraryApi.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GamesLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _service;
        private readonly IMapper _mapper;

        public UserController(IUserRepository service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        ///     Return all users with basic information
        /// </summary>
        [HttpGet, Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(ICollection<ShowUserDto>))]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _service.GetAllUsers();
            var usersMap = _mapper.Map<List<ShowUserDto>>(users);
            return Ok(usersMap);
        }

        /// <summary>
        ///     Return user with basic information and games owned.
        /// </summary>
        [HttpGet("{userId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ShowUserDto>> GetUserById(int userId)
        {
            var user = await _service.GetById(userId);
            if (user == null) return NotFound();
            var userMap = _mapper.Map<ShowUserDto>(user);
            return Ok(userMap);
        }

        /// <summary>
        ///     Add a new user
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddUser(UserDto user)
        {
            var checkUserName = await _service
                .CheckUsernameExists(user.Username!);

            var checkEmail = await _service
                .CheckEmailExists(user.Email!);

            if (checkUserName)
            {
                ModelState.AddModelError("error",
                    "This username is already taken.");
                return BadRequest(ModelState);
            }

            if (checkEmail)
            {
                ModelState.AddModelError("error",
                    "This email is already used.");
                return BadRequest(ModelState);
            }

            var userMap = _mapper.Map<User>(user);
            if (!await _service.Add(userMap)) return StatusCode(500);
            return Ok("User has been created.");
        }

        /// <summary>
        ///     Login user
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var userLogin = await _service.Login(login.email!, login.password!);
            return Ok(userLogin);
        }

        /// <summary>
        ///     Change username using userId and newUsername
        /// </summary>
        [HttpPut("{userId}/username/"), Authorize]
        public async Task<IActionResult> ChangeUsername(int userId, 
            [FromBody] string newUsername)
        {
            var user = await _service.GetById(userId);
            if (user == null) return NotFound("User not found.");

            bool checkUsername = await _service.CheckUsernameExists(newUsername);
            if (checkUsername)
            {
                ModelState.AddModelError("error", "This username is already taken.");
                return BadRequest(ModelState);
            }

            if(! await _service.UpdateUsername(userId, newUsername)) return StatusCode(500);
            return Ok("Username has been updated.");
        }

        /// <summary>
        ///     Change password using userId and newPassword
        /// </summary>
        [HttpPut("{userId}/password/"), Authorize]
        public async Task<IActionResult> ChangePassword(int userId,
            [FromBody] string newPassword)
        {
            var user = await _service.GetById(userId).ConfigureAwait(false);
            if (user == null) return NotFound("User not found.");

            if(!await _service.UpdatePassword(userId, newPassword))
                return StatusCode(500);

            return Ok("Password has been updated.");
        }

        /// <summary>
        ///     Change email address using userId and newEmail
        /// </summary>
        [HttpPut("{userId}/email/"), Authorize]
        public async Task<IActionResult> ChangeEmail(int userId,
            [FromBody, EmailAddress] string newEmail)
        {
            var user = await _service.GetById(userId);
            if(user == null) return NotFound("User not found.");

            bool checkEmail = await _service.CheckEmailExists(newEmail);
            if(checkEmail)
            {
                ModelState.AddModelError("error", 
                    "This email address is used by another account.");
                return BadRequest(ModelState);
            }

            if (!await _service.UpdateEmail(userId, newEmail)) 
                return StatusCode(500);

            return Ok("Email has been updated.");
        }

        /// <summary>
        ///     Delete a user using userId
        /// </summary>
        [HttpDelete("{userId}"), Authorize]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _service.GetById(userId);
            if (user == null) return NotFound();
            if (!await _service.Delete(userId)) return StatusCode(500);
            return Ok("User has been deleted.");
        }
    }
}
