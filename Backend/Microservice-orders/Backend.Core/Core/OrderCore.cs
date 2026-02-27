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

        public OrderCore(IOrderRepositorie orderRepositorie)
        {
            _OrderRepositorie = orderRepositorie;
        }
        #region Get all orders
        /// <summary>
        /// Get order complete list
        /// </summary>
        /// <returns>List<UserDto> </returns>
        public async Task<GeneralResponse> GetAllOrders()
        {
            var oReturn = new GeneralResponse();

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
        public async Task<GeneralResponse> UpsertOrder(OrderRequest orderIn)
        {
            var oReturn = new GeneralResponse();

                var order = await _OrderRepositorie.GetOrderByIdAsync(orderIn.IdUser);

                if (order == null)
                {
                    var orderDb = _OrderRepositorie.CreateAsync(new OrderRequest()
                    {
                        IdUser = orderIn.IdUser,
                        NameApplicant = orderIn.NameApplicant,
                        EmailApplicant = orderIn.EmailApplicant,
                        Products = orderIn.Products,
                    });

                    oReturn.Data = orderDb;
                    oReturn.Status = (int)ServiceStatusCode.Success;
                    oReturn.Message = $"El pedido se creo exitosamente en el sistema bajo el id {orderDb}.";
                }
                else
                {
                //Logica para revisar estado de pago en microservicio de pago

                var userDb = _OrderRepositorie.UpdateAsync(new OrderRequest()
                {
                    NameApplicant = order.NameApplicant,
                    EmailApplicant = order.EmailApplicant,
                    Products = order.Products,
                });

                oReturn.Data = true;
                oReturn.Status = (int)ServiceStatusCode.Success;
                oReturn.Message = "El pedido se edito exitosamente en el sistema.";
            }


            return oReturn;
        }
        #endregion

        #region Create Order
        /// <summary>
        /// Delete Order
        /// </summary>
        /// <param name="input">OrderRequest</param>
        /// <returns>bool</returns>
        public async Task<GeneralResponse> DeleteOrder(string orderId)
        {
            var oReturn = new GeneralResponse();

            var order = await _OrderRepositorie.DisabledOrderByIdAsync(orderId);

                oReturn.Data = order;
                oReturn.Status = (int)ServiceStatusCode.Success;
                oReturn.Message = $"El pedido se elimino exitosamente en el sistema.";
            
          


            return oReturn;
        }
        #endregion

    }
}
