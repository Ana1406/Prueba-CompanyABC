using Backend.DataBase.DataBase;
using Backend.Domain.Models;
using MongoDB.Driver;


namespace Backend.DataBase.Repositories
{
    public class PaymentRepositorie : IPaymentRepositorie
    {
        private readonly IMongoCollection<PaymentModel> _collection;

        public PaymentRepositorie(DbContext context)
        {
            _collection = context.Payments;
        }

        public async Task<List<PaymentResponse>> GetAllAsync()
        {
            return await _collection.Find(_ => true).Project(payment => new PaymentResponse
            {
                NameOwner = payment.NameOwner,
                Products = payment.Products,
                TotalPrice = payment.TotalPrice,
                Email=payment.EmailOwner,

            }).ToListAsync();
        }

        public async Task CreateAsync(PaymentRequest payment)
        {
            var paymentModel = new PaymentModel()
            {
                NameOwner = payment.NameOwner,
                EmailOwner = payment.EmailOwner,
                TotalPrice= payment.TotalPrice,
                Products = payment.Products,
                CreatedDate = DateTime.Now,
            };
            await _collection.InsertOneAsync(paymentModel);
        }

        public async Task<PaymentModel> GetPaymentByIdAsync(string id)
        {
            return await _collection.Find(u => u.Id == id)
        .FirstOrDefaultAsync();
        }
    }
}
