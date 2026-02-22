using Backend.Domain.Models;
using Backend.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Core.Core.Interfaces
{
    public interface IOrderCore
    {
        /// <summary>
        /// GetAllOrders
        /// </summary>
        Task<GeneralResponse> GetAllOrders();

        /// <summary>
        /// CreateOrder
        /// </summary>
        /// <param name="userIn">OrderRequest</param>
        Task<GeneralResponse> CreateOrder(OrderRequest order);

    }
}
