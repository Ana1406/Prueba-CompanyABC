using Backend.Core.Core;
using Backend.Core.Core.Interfaces;
using Backend.Domain.Models;
using Backend.Domain.Response;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<GeneralResponse<HealthResponse> >> Health()
        {

            return Ok( await _healthCore.HealthAsync());
        }

        /// <summary>
        /// GetStatusProject
        /// </summary>
        /// <returns>StatusResponse</returns>
        [HttpGet]
        [Route("status")]
        public async Task<ActionResult<GeneralResponse<StatusResponse>>> Status()
        {

            return Ok( await _healthCore.StatusAsync());
        }
    }
}
