using Backend.Domain.Models;
using Backend.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Core.Core
{
    public interface IHealthCore
    {
        /// <summary>
        /// GetHealth
        /// </summary>
        Task<GeneralResponse> HealthAsync();

        /// <summary>
        /// GetStatus
        /// </summary>
        Task<GeneralResponse> StatusAsync();

    }
}
