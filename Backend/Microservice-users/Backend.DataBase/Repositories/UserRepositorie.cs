using Backend.DataBase.DataBase;
using Backend.Domain.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Text.RegularExpressions;


namespace Backend.DataBase.Repositories
{
    public class UserRepositorie : IUserRepositorie
    {
        private readonly IMongoCollection<UserModel> _collection;

        public UserRepositorie(DbContext context)
        {
            _collection = context.Usuarios;
        }

        public async Task<List<UserResponse>> GetAllAsync(FilterUsersRequest input)
        {
            var filter = Builders<UserModel>.Filter.Empty;

            // 🔎 Filtro por email si viene
            if (!string.IsNullOrEmpty(input.Email))
            {
                var escapedEmail = Regex.Escape(input.Email);
                filter &= Builders<UserModel>.Filter.Regex(
                    x => x.Email,
                    new MongoDB.Bson.BsonRegularExpression($".*{escapedEmail}.*", "i")
                );
            }

            var users = await _collection
                .Find(filter)
                .Skip((input.page - 1) * input.pageSize)   // paginación
                .Limit(input.pageSize)
                .Project(user => new UserResponse
                {
                    Name = user.Name,
                    Email = user.Email,
                    Rol=user.Rol,
                    CreatedDate=user.CreatedDate

                })
                .ToListAsync();
            return users;   

        }

        public async Task<string> CreateAsync(UserRequest user)
        {
            var userModel = new UserModel()
            {
                Name = user.Name,
                Email = user.EmailIn,
                CreatedDate = DateTime.Now,
                Password=user.Password,
                Rol=user.Rol
            };

            await _collection.InsertOneAsync(userModel);
            return userModel.Id;
        }

        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            return await _collection.Find(u => u.Email == email)
                        .FirstOrDefaultAsync();
        }
    }
}
