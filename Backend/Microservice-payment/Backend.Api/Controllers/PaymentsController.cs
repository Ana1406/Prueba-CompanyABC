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
    public class PaymentsController : ControllerBase
    {
        public readonly IPaymentCore _paymentCore;
        public PaymentsController(IPaymentCore paymentCore)
        {
            _paymentCore = paymentCore;
        }

        /// <summary>
        /// GetPaymentByOrderId
        /// </summary>
        /// <returns><PaymentResponse></returns>
        [HttpGet]
        public async Task<ActionResult<GeneralResponse<PaymentResponse>>> GetPaymentByOrderId(string orderId)
        {
            var response = await _paymentCore.GetPaymentByOrderId(orderId);
            if (response.Status == (int)ServiceStatusCode.Success)
                return Ok(response);
            else
            {
                return BadRequest(response);
            }
        }

        /// <summary>
        /// GetAllPayment
        /// </summary>
        /// <returns>List<PaymentResponse></returns>
        [HttpGet]
        public async Task<ActionResult<GeneralResponse<List<PaymentResponse>>>> GetAllPayment()
        {
            var response = await _paymentCore.GetAllPayments();
            if (response.Status == (int)ServiceStatusCode.Success)
                return Ok(response);
            else
            {
                return BadRequest(response);
            }
        }

        /// <summary>
        /// CreatePayment
        /// </summary>
        /// <param name="payment">PaymentRequest</param>
        /// <returns>bool</returns>
        [HttpPost]
        public async Task<ActionResult<GeneralResponse<bool>>> CreatePayment(PaymentRequest payment)
        {
            var response = await _paymentCore.CreatePayment(payment);
            if (response.Status == (int)ServiceStatusCode.Success)
                return Ok(response);
            else
            {
                return BadRequest(response);
            }
        }

        /// <summary>
        /// CreatePayment
        /// </summary>
        /// <param name="payment">PaymentRequest</param>
        /// <returns>bool</returns>
        [HttpPut]
        public async Task<ActionResult<GeneralResponse<bool>>> UpdatePayment(string idPayment)
        {
            var response = await _paymentCore.UpdateStatusPayment(idPayment);
            if (response.Status == (int)ServiceStatusCode.Success)
                return Ok(response);
            else
            {
                return BadRequest(response);
            }
        }

    }
}
            

