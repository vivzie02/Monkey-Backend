using Microsoft.AspNetCore.Mvc;
using MonkeyServer.DTOs;
using MonkeyServer.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonkeyServer.Controllers
{
    /// <summary>
    /// TextController
    /// </summary>
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Start writing text
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/v1/CreateUser")]
        [Produces("application/json")]
        [SwaggerOperation("CreateUser")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public virtual async Task<IActionResult> CreateUserAsync([FromBody] CreateUserInputDTO createUserInputDTO)
        {
            CreateUserOutputDTO userOutput;
            try
            {
                userOutput = await _userService.CreateUser(createUserInputDTO).ConfigureAwait(false);
            }
            catch(System.Exception ex)
            {
                return StatusCode(500, new {message = "An error occurred while creating the new user"});
            }

            return Ok(userOutput);
        }
    }
}
