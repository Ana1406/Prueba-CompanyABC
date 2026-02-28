using Backend.Domain.Models;
using Backend.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Core.Core.Interfaces
{
    public interface IAuthCore
    {


        /// <summary>
        /// Login
        /// </summary>
        /// <param name="userIn">LoginRequest</param>
        Task<GeneralResponse<string>> Login(LoginRequest login);


    }
}
