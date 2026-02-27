using Backend.DataBase.DataBase;
using Backend.Domain.Models;
using MongoDB.Driver;


namespace Backend.DataBase.Repositories
{
    public interface IUserRepositorie
    {
        Task<List<UserResponse>> GetAllAsync(FilterUsersRequest input);
        Task<string> CreateAsync(UserRequest user);
        Task<UserModel> GetUserByEmailAsync(string email);

    }
 
}
