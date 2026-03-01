using Backend.DataBase.DataBase;
using Backend.Domain.Models;
using MongoDB.Driver;


namespace Backend.DataBase.Repositories
{
    public interface IOrderRepositorie
    {
        Task<List<ProductResponse>> GetAllAsync();
        Task<string> CreateAsync(OrderRequest user);
        Task<string> UpdateAsync(OrderRequest user);
        Task<OrderModel> GetOrderByIdAsync(string idUser);
        Task<ProductResponse> GetOrderByIdOrderAsync(string IdOrder);
        Task<string> DisabledOrderByIdAsync(string orderId);

    }
 
}
