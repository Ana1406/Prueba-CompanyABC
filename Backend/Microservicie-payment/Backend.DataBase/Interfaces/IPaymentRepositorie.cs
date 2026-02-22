using Backend.DataBase.DataBase;
using Backend.Domain.Models;
using MongoDB.Driver;


namespace Backend.DataBase.Repositories
{
    public interface IPaymentRepositorie
    {
        Task<List<PaymentResponse>> GetAllAsync();
        Task CreateAsync(PaymentRequest user);
        Task<PaymentModel> GetPaymentByIdAsync(string id);

    }
 
}
