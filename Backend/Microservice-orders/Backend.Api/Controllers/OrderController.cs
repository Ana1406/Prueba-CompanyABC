using Backend.Core.Core.Interfaces;
using Backend.Domain.Models;
using Backend.Domain.Response;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
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
        public async Task<ActionResult<GeneralResponse>> GetAllOrders()
        {

            return Ok(await _orderCore.GetAllOrders());
        }

        /// <summary>
        /// CreateOrder
        /// </summary>
        /// <param name="order">OrderRequest</param>
        /// <returns>bool</returns>
        [HttpPost]
        public async Task<ActionResult<GeneralResponse>> CreateOrder(OrderRequest order)
        {

            return Ok(await _orderCore.CreateOrder(order));
        }
    }
}
