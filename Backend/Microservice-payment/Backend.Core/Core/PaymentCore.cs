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
        public async Task<GeneralResponse> GetAllPayments()
        {
            var oReturn = new GeneralResponse();

            try
            {
                var usersList = await _PaymentRepositorie.GetAllAsync();

                oReturn.Data = usersList;
                oReturn.Status = (int)ServiceStatusCode.Success;
                oReturn.Message = $"Se encontraron {usersList.Count} registros de pagos en el sistema.";
            }
            catch (Exception ex)
            {
                oReturn.Message = "Error obteniendo los pagos registrados en el sistema";
                oReturn.Status = (int)ServiceStatusCode.ValidationError;
            }

            return oReturn;
        }
        #endregion

        #region Create Payment
        /// <summary>
        /// Create  payment
        /// </summary>
        /// <param name="payment">PaymentRequest</param>
        /// <returns>bool</returns>
        public async Task<GeneralResponse> CreatePayment(PaymentRequest payment)
        {
            var oReturn = new GeneralResponse();

            try
            {

                    var paymentDb = _PaymentRepositorie.CreateAsync(new PaymentRequest()
                    {
                        NameOwner = payment.NameOwner,
                        EmailOwner = payment.EmailOwner,
                        Products = payment.Products,
                        TotalPrice = payment.TotalPrice,
                    });

                oReturn.Data = true;
                oReturn.Status = (int)ServiceStatusCode.Success;
                oReturn.Message = "El pago se realizo exitosamente en el sistema.";

            }
            catch (Exception ex)
            {
                oReturn.Message = "Error procesando pago en el sistema.";
                oReturn.Data = false;
                oReturn.Status = (int)ServiceStatusCode.InternalError;
            }

            return oReturn;
        }
        #endregion

    }
}
