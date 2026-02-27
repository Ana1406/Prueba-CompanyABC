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
        /// UpsertOrder
        /// </summary>
        /// <param name="userIn">OrderRequest</param>
        Task<GeneralResponse> UpsertOrder(OrderRequest order);

        /// <summary>
        /// DeleteOrder
        /// </summary>
        /// <param name="orderID">OrderRequest</param>
        Task<GeneralResponse> DeleteOrder(string orderID);

    }
}
