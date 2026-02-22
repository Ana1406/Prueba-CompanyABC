using Backend.DataBase.DataBase;
using Backend.Domain.Models;
using MongoDB.Driver;


namespace Backend.DataBase.Repositories
{
    public class OrderRepositorie : IOrderRepositorie
    {
        private readonly IMongoCollection<OrderModel> _collection;

        public OrderRepositorie(DbContext context)
        {
            _collection = context.Pedidos;
        }

        public async Task<List<ProductResponse>> GetAllAsync()
        {
            return await _collection.Find(_ => true).Project(order => new ProductResponse
            {
                Products = order.Products,
                NameApplicant = order.NameApplicant,
                EmailApplicant = order.EmailApplicant,
            }).ToListAsync();
        }

        public async Task CreateAsync(OrderRequest order)
        {
            var orderModel = new OrderModel()
            {
                NameApplicant = order.NameApplicant,
                EmailApplicant = order.EmailApplicant,
                Products = order.Products,
                CreatedDate = DateTime.Now,
            };
            await _collection.InsertOneAsync(orderModel);
        }

        public async Task<OrderModel> GetOrderByEmailAsync(string email)
        {
            return await _collection.Find(u => u.EmailApplicant == email)
        .FirstOrDefaultAsync();
        }
    }
}
