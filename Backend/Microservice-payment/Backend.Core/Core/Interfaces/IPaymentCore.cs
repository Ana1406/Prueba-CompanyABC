using Backend.Domain.Models;
using Backend.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Core.Core.Interfaces
{
    public interface IPaymentCore
    {
        /// <summary>
        /// GetAllPayment
        /// </summary>
        Task<GeneralResponse<List<PaymentResponse>>> GetAllPayments();

        /// <summary>
        /// Get Payment By Order Id
        /// </summary>
        /// <returns><PaymentDto> </returns>
        Task<GeneralResponse<PaymentResponse>> GetPaymentByOrderId(string orderId);

        /// <summary>
        /// CreatePayment
        /// </summary>
        /// <param name="payment">PaymentRequest</param>
        Task<GeneralResponse<bool>> CreatePayment(PaymentRequest payment);

        /// <summary>
        /// Update Status Payment
        /// </summary>
        /// <param name="payment">PaymentRequest</param>
        Task<GeneralResponse<bool>> UpdateStatusPayment(string idPayment);
    }
}
