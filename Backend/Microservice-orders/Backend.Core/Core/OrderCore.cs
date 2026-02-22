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

            try
            {
                var ordersList = await _OrderRepositorie.GetAllAsync();

                oReturn.Data = ordersList;
                oReturn.Status = (int)ServiceStatusCode.Success;
                oReturn.Message = $"Se encontraron {ordersList.Count} registros de ordenes en el sistema.";
            }
            catch (Exception ex)
            {
                oReturn.Message = "Error obteniendo los ordenes registrados en el sistema";
                oReturn.Status = (int)ServiceStatusCode.InternalError;
            }

            return oReturn;
        }
        #endregion

        #region Create Order
        /// <summary>
        /// Create Order
        /// </summary>
        /// <param name="input">OrderRequest</param>
        /// <returns>bool</returns>
        public async Task<GeneralResponse> CreateOrder(OrderRequest order)
        {
            var oReturn = new GeneralResponse();

            try
            {
                var users = new List<OrderModel>();

                var user = await _OrderRepositorie.GetOrderByEmailAsync(order.EmailApplicant);

                if (user == null)
                {
                    var userDb = _OrderRepositorie.CreateAsync(new OrderRequest()
                    {
                        NameApplicant = order.NameApplicant,
                        EmailApplicant = order.EmailApplicant,
                        Products = order.Products,
                    });

                    oReturn.Data = true;
                    oReturn.Status = (int)ServiceStatusCode.Success;
                    oReturn.Message = "El pedido se creo exitosamente en el sistema.";
                }
                else
                {
                    oReturn.Message = "El pedido ya se encuentra registrado en el sistema.";
                    oReturn.Data = false;
                    oReturn.Status = (int)ServiceStatusCode.ValidationError;
                }

            }
            catch (Exception ex)
            {
                oReturn.Message = "Error creando pedido en el sistema.";
                oReturn.Data = false;
             
            }

            return oReturn;
        }
        #endregion

    }
}
