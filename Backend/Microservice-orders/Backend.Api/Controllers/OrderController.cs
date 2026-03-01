using Backend.Core.Core.Interfaces;
using Backend.Domain.Models;
using Backend.Domain.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        public readonly IOrderCore _orderCore;
        public OrdersController(IOrderCore orderCore)
        {
            _orderCore = orderCore;
        }

        /// <summary>
        /// GetAllOrders
        /// </summary>
        /// <returns>List<OrderResponse></returns>
        [HttpGet]
        public async Task<ActionResult<GeneralResponse<List<ProductResponse>>>> GetAllOrders()
        {

            return Ok(await _orderCore.GetAllOrders());
        }

        /// <summary>
        /// GetOrderByOrderId
        /// </summary>
        /// <returns>List<OrderResponse></returns>
        [HttpGet]
        public async Task<ActionResult<GeneralResponse<List<ProductResponse>>>> GetOrderByOrderId(string orderId)
        {

            return Ok(await _orderCore.GetOrderByOrderId(orderId));
        }


        /// <summary>
        /// CreateOrder
        /// </summary>
        /// <param name="order">OrderRequest</param>
        /// <returns>bool</returns>
        [HttpPost]
        public async Task<ActionResult<GeneralResponse<string>>> CreateOrder([FromBody] OrderRequest order)
        {

            return Ok(await _orderCore.CreateOrder(order));
        }

        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="order">OrderRequest</param>
        /// <returns>bool</returns>
        [HttpPut]
        public async Task<ActionResult<GeneralResponse<bool>>> EditOrder([FromBody] OrderRequest order)
        {

            return Ok(await _orderCore.EditOrder(order));
        }

        /// <summary>
        /// DeleteOrder
        /// </summary>
        /// <param name="order">OrderId</param>
        /// <returns>bool</returns>
        [HttpPost]
        public async Task<ActionResult<GeneralResponse<string>>> DeleteOrder([FromBody] DeleteOrderRequest orderId)
        {

            return Ok(await _orderCore.DeleteOrder(orderId.IdOrder));
        }
    }
}
