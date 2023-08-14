using AutoMapper;
using GamesLibraryApi.Dto.Users;
using GamesLibraryApi.Interfaces;
using GamesLibraryApi.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace GamesLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _service;
        private readonly IGameRepository _gameService;
        private readonly IMapper _mapper;

        public UserController(IUserRepository service, IGameRepository gameService, IMapper mapper)
        {
            _service = service;
            _gameService = gameService;
            _mapper = mapper;
        }

        /// <summary>
        ///     Return all users with basic information
        /// </summary>
        [HttpGet, Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(ICollection<ShowUsersDto>))]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _service.GetAllUsers();
            var usersMap = _mapper.Map<List<ShowUsersDto>>(users);
            return Ok(usersMap);
        }

        /// <summary>
        ///     Return user and games owned.
        /// </summary>
        /// <param name="userId">Id of user</param>
        [HttpGet("{userId}"), Authorize]
        public async Task<ActionResult<ShowUserDto>> GetUserById(int userId)
        {
            var user = await _service.GetById(userId);
            if (user == null) return NotFound();

            var userMap = _mapper.Map<ShowUserDto>(user);
            userMap.GamePurchases = _mapper.Map<ICollection<UserGamePurchaseDto>>(user.UserGamePurchases);

            return Ok(userMap);
        }

        /// <summary>
        ///     Add a new user
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
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

            bool addUser = await _service.Add(userMap);
            if (!addUser) return StatusCode(500);

            return Ok("User has been created.");
        }

        /// <summary>
        ///     Login user
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var userLogin = await _service.Login(login.email!, login.password!);
            return Ok(userLogin);
        }

        /// <summary>
        ///     Add game to user using userId and gameId
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="gameId">Id of game</param>
        [HttpPost("{userId}/game/{gameId}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddGameToUser(int userId, int gameId)
        {
            var user = await _service.GetById(userId).ConfigureAwait(false);
            var game = await _gameService.GetById(gameId).ConfigureAwait(false);

            if (user == null) return NotFound("User not found.");
            if (game == null) return NotFound("Game not found.");

            bool UserOwnGame = user.UserGamePurchases.Any(g => g.GameId == gameId);
            if (UserOwnGame) return BadRequest("User already own this game.");

            bool addGameToUser = await _service.AddGameToUser(userId, gameId);
            if (!addGameToUser) return StatusCode(500);

            return Ok("Game has been added to user.");
        }

        /// <summary>
        ///     Change your username using newUsername
        /// </summary>
        [HttpPut("username/"), Authorize]
        public async Task<IActionResult> ChangeUsername
            ([FromBody] string newUsername)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if(userIdClaim == null) 
                return BadRequest("User ID not found in token.");
            var userId = int.Parse(userIdClaim.Value);

            var user = await _service.GetById(userId).ConfigureAwait(false);
            if (user == null) return NotFound("User not found.");

            bool checkUsername = await _service.CheckUsernameExists(newUsername);
            if (checkUsername)
            {
                ModelState.AddModelError(
                    "error", "This username is already taken."
                    );
                return BadRequest(ModelState);
            }

            bool upddateUsername = await _service.UpdateUsername(userId, newUsername);
            if (!upddateUsername) return StatusCode(500);

            return Ok("Username has been updated.");
        }

        /// <summary>
        ///     Change your password
        /// </summary>
        [HttpPut("password/"), Authorize]
        public async Task<IActionResult> ChangePassword(
            ChangePassword change)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return BadRequest("User ID not found in token.");
            var userId = int.Parse(userIdClaim.Value);

            var user = await _service.GetById(userId).ConfigureAwait(false);
            if (user == null) return NotFound("User not found.");

            var checkOld = BCrypt.Net.BCrypt
                .Verify(change.OldPassword, user.Password);
            if (!checkOld) return BadRequest("Old password not match.");

            if (change.OldPassword == change.NewPassword) 
                return BadRequest("You cannot use the old password" +
                    " as the new password.");

            bool updatePassword = await _service
                .UpdatePassword(userId, change.NewPassword!);
            if (!updatePassword) return StatusCode(500);

            return Ok("Password has been updated.");
        }

        /// <summary>
        ///     Change your email address using newEmail
        /// </summary>
        [HttpPut("email/"), Authorize]
        public async Task<IActionResult> ChangeEmail(
            [FromBody, EmailAddress] string newEmail)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return BadRequest("User ID not found in token.");
            var userId = int.Parse(userIdClaim.Value);

            var user = await _service.GetById(userId);
            if(user == null) return NotFound("User not found.");

            bool checkEmail = await _service.CheckEmailExists(newEmail);
            if(checkEmail)
            {
                ModelState.AddModelError("error", 
                    "This email address is used by another account.");
                return BadRequest(ModelState);
            }

            bool updateEmail = await _service.UpdateEmail(userId, newEmail);
            if (!updateEmail) return StatusCode(500);

            return Ok("Email has been updated.");
        }

        /// <summary>
        ///     Delete your account
        /// </summary>
        [HttpDelete, Authorize]
        public async Task<IActionResult> DeleteUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return BadRequest("User ID not found in token.");
            var userId = int.Parse(userIdClaim.Value);

            var user = await _service.GetById(userId);
            if (user == null) return NotFound();

            bool deleteUser = await _service.Delete(userId);
            if (!deleteUser) return StatusCode(500);

            return Ok("User has been deleted.");
        }
    }
}
