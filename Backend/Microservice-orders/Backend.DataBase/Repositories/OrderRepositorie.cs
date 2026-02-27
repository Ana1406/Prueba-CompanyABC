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
            return await _collection.Find(order => order.Enabled).Project(order => new ProductResponse
            {
                IdOrder=order.IdOrder,
                Products = order.Products,
                NameApplicant = order.NameApplicant,
                EmailApplicant = order.EmailApplicant,
            }).ToListAsync();
        }

        public async Task<string> CreateAsync(OrderRequest order)
        {
            var orderModel = new OrderModel()
            {
                IdUser = order.IdUser,
                NameApplicant = order.NameApplicant,
                EmailApplicant = order.EmailApplicant,
                Products = order.Products,
                CreatedDate = DateTime.Now,
                Enabled = true
            };
            await _collection.InsertOneAsync(orderModel);
            return orderModel.IdOrder;
        }

        public async Task<string> UpdateAsync(OrderRequest order)
        {
            var orderExits = Builders<OrderModel>.Filter.Eq(x => x.IdOrder, order.IdOrder);

            var orderModel = new OrderModel()
            {
                IdUser = order.IdUser,
                NameApplicant = order.NameApplicant,
                EmailApplicant = order.EmailApplicant,
                Products = order.Products,
                Enabled = true
            };

            await _collection.ReplaceOneAsync(orderExits, orderModel);
            return orderModel.IdOrder;
        }

        public async Task<string> DisabledOrderByIdAsync(string orderId)
        {
            var orderExits = Builders<OrderModel>.Filter.Eq(x => x.IdOrder, orderId);

            var update = Builders<OrderModel>.Update
                .Set(x => x.Enabled, false);

            await _collection.UpdateOneAsync(orderExits, update);

            return orderId;
        }

        public async Task<OrderModel> GetOrderByIdAsync(string idUser)
        {
            return await _collection.Find(u => u.IdUser == idUser)
        .FirstOrDefaultAsync();
        }
    }
}
