using Backend.Core.Core.Interfaces;
using Backend.Domain.Models;
using Backend.Domain.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Backend.Domain.Models.Enums;

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

            var response = await _orderCore.GetAllOrders();
            if (response.Status == (int)ServiceStatusCode.Success)
                return Ok(response);
            else
            {
                return BadRequest(response);
            }
        }

        /// <summary>
        /// GetOrderByOrderId
        /// </summary>
        /// <returns>List<OrderResponse></returns>
        [HttpGet]
        public async Task<ActionResult<GeneralResponse<List<ProductResponse>>>> GetOrderByOrderId(string orderId)
        {

            var response = await _orderCore.GetOrderByOrderId(orderId);
            if (response.Status == (int)ServiceStatusCode.Success)
                return Ok(response);
            else
            {
                return BadRequest(response);
            }
        }


        /// <summary>
        /// CreateOrder
        /// </summary>
        /// <param name="order">OrderRequest</param>
        /// <returns>bool</returns>
        [HttpPost]
        public async Task<ActionResult<GeneralResponse<string>>> CreateOrder([FromBody] OrderRequest order)
        {

            var response = await _orderCore.CreateOrder(order);
            if (response.Status == (int)ServiceStatusCode.Success)
                return Ok(response);
            else
            {
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="order">OrderRequest</param>
        /// <returns>bool</returns>
        [HttpPut]
        public async Task<ActionResult<GeneralResponse<bool>>> EditOrder([FromBody] OrderRequest order)
        {

            var response = await _orderCore.EditOrder(order);
            if (response.Status == (int)ServiceStatusCode.Success)
                return Ok(response);
            else
            {
                return BadRequest(response);
            }
        }

        /// <summary>
        /// DeleteOrder
        /// </summary>
        /// <param name="order">OrderId</param>
        /// <returns>bool</returns>
        [HttpPost]
        public async Task<ActionResult<GeneralResponse<string>>> DeleteOrder([FromBody] DeleteOrderRequest orderId)
        {

            var response = await _orderCore.DeleteOrder(orderId.IdOrder);
            if (response.Status == (int)ServiceStatusCode.Success)
                return Ok(response);
            else
            {
                return BadRequest(response);
            }
        }
    }
}
