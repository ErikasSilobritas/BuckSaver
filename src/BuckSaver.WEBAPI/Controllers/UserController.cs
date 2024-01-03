using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BuckSaver.WEBAPI.Controllers
{
    [ApiController()]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            return Ok(await _userService.GetUsers());
        }

        /// <summary>
        /// Get a user by id
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GetUser>> GetUser(int id)
        {
            return Ok(await _userService.GetUser(id));
        }

        /// <summary>
        /// Create a user
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CreateUser>> CreateUser([FromBody] CreateUser createUser)
        {
            var createdUser = await _userService.CreateUser(createUser);
            return Ok(createdUser);
        }

    }
}
