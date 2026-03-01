using Backend.Core.Core;
using Backend.Core.Core.Interfaces;
using Backend.Domain.Models;
using Backend.Domain.Response;
using Microsoft.AspNetCore.Mvc;
using static Backend.Domain.Models.Enums;

namespace Backend.Api.Controllers
{
    [ApiController]
    public class HealthController : ControllerBase
    {
        public readonly IHealthCore _healthCore;
        public HealthController(IHealthCore healthCore)
        {
            _healthCore = healthCore;
        }

        /// <summary>
        /// GetHealthProject
        /// </summary>
        /// <returns>HealthResponse</returns>
        [HttpGet]
        [Route("health")]
        public async Task<ActionResult<GeneralResponse<HealthResponse>>> Health()
        {

            var response = await  _healthCore.HealthAsync();
            if (response.Status == (int)ServiceStatusCode.Success)
                return Ok(response);
            else
            {
                return BadRequest(response);
            }
        }

        /// <summary>
        /// GetStatusProject
        /// </summary>
        /// <returns>StatusResponse</returns>
        [HttpGet]
        [Route("status")]
        public async Task<ActionResult<GeneralResponse<StatusResponse>>> Status()
        {
            var response = await _healthCore.StatusAsync();
            if (response.Status == (int)ServiceStatusCode.Success)
                return Ok(response);
            else
            {
                return BadRequest(response);
            }
        }
    }
}
