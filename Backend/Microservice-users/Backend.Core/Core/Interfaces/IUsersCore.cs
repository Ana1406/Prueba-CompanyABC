using Backend.Domain.Models;
using Backend.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Core.Core.Interfaces
{
    public interface IUsersCore
    {
        /// <summary>
        /// GetAllUsers
        /// </summary>
        Task<GeneralResponse<List<UserResponse>>> GetAllUsers(FilterUsersRequest input);

        /// <summary>
        /// CreateUser
        /// </summary>
        /// <param name="userIn">UserRequest</param>
        Task<GeneralResponse<string>> CreateUser(UserRequest userIn);

        /// <summary>
        /// Get userId by email
        /// </summary>
        /// <returns><UserDto> </returns>
        Task<GeneralResponse<string>> GetUserIdByEmail(string email);

    }
}
