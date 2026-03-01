using Backend.Domain.Models;
using Backend.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Core.Core.Interfaces
{
    public interface IOrderServices
    {

        /// <summary>
        /// Get Payment by order id
        /// </summary>
        /// <param name="IdOrder">string</param>
        Task<ProductResponse> GetOrderByOrderId(string IdOrder);


    }
}
