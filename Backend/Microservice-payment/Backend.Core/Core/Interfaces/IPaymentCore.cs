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
        Task<GeneralResponse> GetAllPayments();

        /// <summary>
        /// CreatePayment
        /// </summary>
        /// <param name="payment">PaymentRequest</param>
        Task<GeneralResponse> CreatePayment(PaymentRequest payment);

    }
}
