using Backend.Core.Core.Interfaces;
using Backend.DataBase.Repositories;
using Backend.Domain.Models;
using Backend.Domain.Response;
using static Backend.Domain.Models.Enums;


namespace Backend.Core.Core
{
    
    public class PaymentCore : IPaymentCore
    {
        private readonly IPaymentRepositorie _PaymentRepositorie;

        public PaymentCore(IPaymentRepositorie paymentRepositorie)
        {
            _PaymentRepositorie = paymentRepositorie;
        }
        #region Get all payments
        /// <summary>
        /// Get payment complete list
        /// </summary>
        /// <returns>List<PaymentDto> </returns>
        public async Task<GeneralResponse<List<PaymentResponse>>> GetAllPayments()
        {
            var oReturn = new GeneralResponse<List<PaymentResponse>>();

                var paymentsList = await _PaymentRepositorie.GetAllAsync();

                oReturn.Data = paymentsList;
                oReturn.Status = (int)ServiceStatusCode.Success;
                oReturn.Message = $"Se encontraron {paymentsList.Count} registros de pagos en el sistema.";

            return oReturn;
        }
        #endregion

        #region Get Payment By Order Id
        /// <summary>
        /// Get Payment By Order Id
        /// </summary>
        /// <returns><PaymentDto> </returns>
        public async Task<GeneralResponse<PaymentResponse>> GetPaymentByOrderId(string orderId)
        {
            var oReturn = new GeneralResponse<PaymentResponse>();

            var payment = await _PaymentRepositorie.GetPaymentByIdOrderAsync(orderId);

            oReturn.Data = payment;
            oReturn.Status = (int)ServiceStatusCode.Success;
            oReturn.Message = $"Se encontro el registro de pago en el sistema.";

            return oReturn;
        }
        #endregion

        #region Create Payment
        /// <summary>
        /// Create  payment
        /// </summary>
        /// <param name="payment">PaymentRequest</param>
        /// <returns>bool</returns>
        public async Task<GeneralResponse<bool>> CreatePayment(PaymentRequest payment)
        {
            var oReturn = new GeneralResponse<bool>();

                    var paymentDb = _PaymentRepositorie.CreateAsync(payment);

                oReturn.Data = true;
                oReturn.Status = (int)ServiceStatusCode.Success;
                oReturn.Message = "El pago se realizo exitosamente en el sistema.";

            return oReturn;
        }
        #endregion

        #region Update Status Payment
        /// <summary>
        /// Update Status Payment
        /// </summary>
        /// <param name="payment">PaymentRequest</param>
        /// <returns>bool</returns>
        public async Task<GeneralResponse<bool>> UpdateStatusPayment(string idPayment)
        {
            var oReturn = new GeneralResponse<bool>();

            var paymentDb = _PaymentRepositorie.UpdateStatusAsync(idPayment);

            oReturn.Data = true;
            oReturn.Status = (int)ServiceStatusCode.Success;
            oReturn.Message = "El pago se actualizo exitosamente en el sistema.";

            return oReturn;
        }
        #endregion

    }
}
