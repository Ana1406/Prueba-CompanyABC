using Backend.Core.Core.Interfaces;
using Backend.DataBase.Repositories;
using Backend.Domain.Models;
using Backend.Domain.Response;
using static Backend.Domain.Models.Enums;


namespace Backend.Core.Core
{
    
    public class OrderCore : IOrderCore
    {
        private readonly IOrderRepositorie _OrderRepositorie;
        private readonly IPaymentServices _PaymentServices;

        public OrderCore(IOrderRepositorie orderRepositorie, IPaymentServices PaymentServices)
        {
            _OrderRepositorie = orderRepositorie;
            _PaymentServices = PaymentServices;
        }
        #region Get all orders
        /// <summary>
        /// Get order complete list
        /// </summary>
        /// <returns>List<UserDto> </returns>
        public async Task<GeneralResponse<List<ProductResponse>>> GetAllOrders()
        {
            var oReturn = new GeneralResponse<List<ProductResponse>>();

                var ordersList = await _OrderRepositorie.GetAllAsync();

                oReturn.Data = ordersList;
                oReturn.Status = (int)ServiceStatusCode.Success;
                oReturn.Message = $"Se encontraron {ordersList.Count} registros de ordenes en el sistema.";

            return oReturn;
        }
        #endregion

        #region Create Order
        /// <summary>
        /// Create Order
        /// </summary>
        /// <param name="input">OrderRequest</param>
        /// <returns>bool</returns>
        public async Task<GeneralResponse<string>> CreateOrder(OrderRequest orderIn)
        {
            var oReturn = new GeneralResponse<string>();


                    var orderDb = await _OrderRepositorie.CreateAsync(new OrderRequest()
                    {
                        IdUser = orderIn.IdUser,
                        NameApplicant = orderIn.NameApplicant,
                        EmailApplicant = orderIn.EmailApplicant,
                        Products = orderIn.Products,
                        IdOrder= orderIn.IdOrder,
                    });
            if (orderDb != null)
            {
                _PaymentServices.CreatePaymentAsync(orderIn, orderDb);
            }

                    oReturn.Data = orderDb;
                    oReturn.Status = (int)ServiceStatusCode.Success;
                    oReturn.Message = $"El pedido se creo exitosamente en el sistema bajo el id {orderDb}.";

            return oReturn;
        }
        #endregion

        #region Update Order
        /// <summary>
        /// Create Order
        /// </summary>
        /// <param name="input">OrderRequest</param>
        /// <returns>bool</returns>
        public async Task<GeneralResponse<bool>> EditOrder(OrderRequest orderIn)
        {
            var oReturn = new GeneralResponse<bool>();

            //Logica para revisar estado de pago en microservicio de pago
            var payment = await _PaymentServices.GetPaymentByOrderId(orderIn.IdOrder);
            if (payment!=null && !payment.Status  ) {
                var userDb = _OrderRepositorie.UpdateAsync(new OrderRequest()
                {
                    NameApplicant = orderIn.NameApplicant,
                    EmailApplicant = orderIn.EmailApplicant,
                    Products = orderIn.Products,
                    IdUser=orderIn.IdUser,
                    IdOrder=orderIn.IdOrder

                });

                oReturn.Data = true;
                oReturn.Status = (int)ServiceStatusCode.Success;
                oReturn.Message = "El pedido se edito exitosamente en el sistema.";
                return oReturn;
            }

            oReturn.Data = false;
            oReturn.Status = (int)ServiceStatusCode.ValidationError;
            oReturn.Message = "El pedido se ya se encuentra pagado. No es posible editar.";
            return oReturn;



        }
        #endregion

        #region Delete Order
        /// <summary>
        /// Delete Order
        /// </summary>
        /// <param name="input">OrderRequest</param>
        /// <returns>bool</returns>
        public async Task<GeneralResponse<string>> DeleteOrder(string orderId)
        {
            var oReturn = new GeneralResponse<string>();
            var payment = await _PaymentServices.GetPaymentByOrderId(orderId);
            if (!payment.Status)
            {
                var order = await _OrderRepositorie.DisabledOrderByIdAsync(orderId);

                oReturn.Data = order;
                oReturn.Status = (int)ServiceStatusCode.Success;
                oReturn.Message = $"El pedido se elimino exitosamente en el sistema.";
                return oReturn;
            }
            oReturn.Data = null;
            oReturn.Status = (int)ServiceStatusCode.ValidationError;
            oReturn.Message = $"El pedido se ya se encuentra pagado. No es posible ejecutar la accion.";
            return oReturn;
        }
        #endregion

    }
}
