using Backend.DataBase.DataBase;
using Backend.Domain.Models;
using MongoDB.Driver;


namespace Backend.DataBase.Repositories
{
    public interface IPaymentRepositorie
    {
        Task<List<PaymentResponse>> GetAllAsync();
        Task<string> CreateAsync(PaymentRequest user);

        Task<string> UpdateStatusAsync(string IdPayment);
        Task<PaymentResponse> GetPaymentByIdOrderAsync(string idOrder);

    }
 
}
