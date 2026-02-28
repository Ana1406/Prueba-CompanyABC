using Backend.Core.Core.Interfaces;
using Backend.Domain.Enums;
using Backend.Domain.Models;
using Backend.Domain.Response;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthCore _authCore;
        public readonly IUsersCore _userCore;
        public AuthController(IAuthCore authCore, IUsersCore userCore   )
        {
            _authCore = authCore;
            _userCore = userCore;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login">LoginRequest</param>
        /// <returns>string</returns>
        [HttpPost]
        public async Task<ActionResult<GeneralResponse<string>>> Login(LoginRequest login)
        {
            var user = await _userCore.GetUserIdByEmail(login.Email);
            if (user.Data!= null)
            {
                var result = await _authCore.Login(login);
                if (result.Status == (int)ServiceStatusCode.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            return Unauthorized();  
             
            
            ;
        }
    }
}
