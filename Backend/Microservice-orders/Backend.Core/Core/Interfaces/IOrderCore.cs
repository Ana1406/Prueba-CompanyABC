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
        Task<GeneralResponse<List<ProductResponse>>> GetAllOrders();

        /// <summary>
        /// Create Order
        /// </summary>
        /// <param name="userIn">OrderRequest</param>
        Task<GeneralResponse<string>> CreateOrder(OrderRequest order);

        /// <summary>
        /// Create Order
        /// </summary>
        /// <param name="userIn">OrderRequest</param>
        Task<GeneralResponse<bool>> EditOrder(OrderRequest order);

        /// <summary>
        /// DeleteOrder
        /// </summary>
        /// <param name="orderID">OrderRequest</param>
        Task<GeneralResponse<string>> DeleteOrder(string orderID);

    }
}
