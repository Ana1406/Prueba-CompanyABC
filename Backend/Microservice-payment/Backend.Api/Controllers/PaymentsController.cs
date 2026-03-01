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
    public class PaymentsController : ControllerBase
    {
        public readonly IPaymentCore _paymentCore;
        public PaymentsController(IPaymentCore paymentCore)
        {
            _paymentCore=paymentCore;
        }

        /// <summary>
        /// GetPaymentByOrderId
        /// </summary>
        /// <returns><PaymentResponse></returns>
        [HttpGet]
        public async Task<ActionResult<GeneralResponse<PaymentResponse>>> GetPaymentByOrderId(string orderId)
        {

            return Ok(await _paymentCore.GetPaymentByOrderId(orderId));
        }

        /// <summary>
        /// GetAllPayment
        /// </summary>
        /// <returns>List<PaymentResponse></returns>
        [HttpGet]
        public async Task<ActionResult<GeneralResponse<List<PaymentResponse>>>> GetAllPayment()
        {

            return Ok(await _paymentCore.GetAllPayments());
        }

        /// <summary>
        /// CreatePayment
        /// </summary>
        /// <param name="payment">PaymentRequest</param>
        /// <returns>bool</returns>
        [HttpPost]
        public async Task<ActionResult<GeneralResponse<bool>>> CreatePayment(PaymentRequest payment)
        {
            return Ok(await _paymentCore.CreatePayment(payment));
        }

        /// <summary>
        /// CreatePayment
        /// </summary>
        /// <param name="payment">PaymentRequest</param>
        /// <returns>bool</returns>
        [HttpPut]
        public async Task<ActionResult<GeneralResponse<bool>>> UpdatePayment(string idPayment)
        {
            return Ok(await _paymentCore.UpdateStatusPayment(idPayment));
        }
    }
}
