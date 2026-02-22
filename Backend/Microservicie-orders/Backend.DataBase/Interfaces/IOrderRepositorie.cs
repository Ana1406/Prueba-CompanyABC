using Backend.DataBase.DataBase;
using Backend.Domain.Models;
using MongoDB.Driver;


namespace Backend.DataBase.Repositories
{
    public interface IOrderRepositorie
    {
        Task<List<ProductResponse>> GetAllAsync();
        Task CreateAsync(OrderRequest user);
        Task<OrderModel> GetOrderByEmailAsync(string email);

    }
 
}
