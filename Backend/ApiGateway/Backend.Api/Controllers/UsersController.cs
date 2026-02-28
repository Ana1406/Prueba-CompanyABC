using Backend.Core.Core.Interfaces;
using Backend.Domain.Enums;
using Backend.Domain.Models;
using Backend.Domain.Response;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly IUsersCore _userCore;
        public UsersController(IUsersCore userCore)
        {
            _userCore=userCore;
        }

        /// <summary>
        /// GetAllUsers
        /// </summary>
        /// <returns>List<UserResponse></returns>
        [HttpGet]
        public async Task<ActionResult<GeneralResponse>> GetAllUsers([FromQuery] string? email,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
        {
            FilterUsersRequest filter = new FilterUsersRequest()
            {
                Email = email,
                page = page,
                pageSize = pageSize
            };
            var result = await _userCore.GetAllUsers(filter);
            if(result.Status == (int)ServiceStatusCode.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
            
        }

        /// <summary>
        /// Get userId by email
        /// </summary>
        /// <returns><UserDto> </returns>
        [HttpGet]
        public async Task<ActionResult<GeneralResponse>> GetUserIdByEmail([FromQuery] string? email)
        {
           
            var result = await _userCore.GetUserIdByEmail(email);
            if (result.Status == (int)ServiceStatusCode.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }

        /// <summary>
        /// CreateUser
        /// </summary>
        /// <param name="userIn">UserRequest</param>
        /// <returns>bool</returns>
        [HttpPost]
        public async Task<ActionResult<GeneralResponse>> CreateUser(UserRequest userIn)
        {
            var result = await _userCore.CreateUser(userIn);
            if (result.Status == (int)ServiceStatusCode.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
            ;
        }
    }
}
