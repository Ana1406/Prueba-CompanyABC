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
                IdPayment=payment.IdPayment,
                NameOwner = payment.NameOwner,
                Products = payment.Products,
                TotalPrice = payment.TotalPrice,
                Email=payment.EmailOwner,
                Status=payment.StatusPayment


            }).ToListAsync();
        }

        public async Task<string> CreateAsync(PaymentRequest payment)
        {
            var paymentModel = new PaymentModel()
            {
                NameOwner = payment.NameOwner,
                IdOrder=payment.IdOrder,
                EmailOwner = payment.EmailOwner,
                TotalPrice= payment.TotalPrice,
                Products = payment.Products,
                CreatedDate = DateTime.Now,
            };
            await _collection.InsertOneAsync(paymentModel);
            return paymentModel.IdPayment;
        }

        public async Task<string> UpdateStatusAsync(string IdPayment)
        {
            var payment = Builders<PaymentModel>.Filter.Eq(x => x.IdPayment, IdPayment);

            var update = Builders<PaymentModel>.Update
                .Set(x => x.StatusPayment, true);

            await _collection.UpdateOneAsync(payment, update);
            
            return IdPayment;
        }

        public async Task<PaymentResponse> GetPaymentByIdOrderAsync(string idOrder)
        {
            return await _collection.Find(u => u.IdOrder == idOrder).Project(payment => new PaymentResponse
            {
                IdPayment = payment.IdPayment,
                NameOwner = payment.NameOwner,
                Products = payment.Products,
                TotalPrice = payment.TotalPrice,
                Email = payment.EmailOwner,
                Status = payment.StatusPayment
            })
    .FirstOrDefaultAsync();
        }
    }
}
