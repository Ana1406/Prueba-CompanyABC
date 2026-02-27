using Backend.Core.Core.Interfaces;
using Backend.DataBase.Repositories;
using Backend.Domain.Enums;
using Backend.Domain.Models;
using Backend.Domain.Response;


namespace Backend.Core.Core
{
    
    public class UsersCore : IUsersCore
    {
        private readonly IUserRepositorie _UserRepositorie;

        public UsersCore(IUserRepositorie userRepositorie)
        {
            _UserRepositorie = userRepositorie;
        }
        #region Get all users
        /// <summary>
        /// Get user complete list
        /// </summary>
        /// <returns>List<UserDto> </returns>
        public async Task<GeneralResponse> GetAllUsers(FilterUsersRequest input)
        {
            var oReturn = new GeneralResponse();

            var usersList = await  _UserRepositorie.GetAllAsync( input);


            oReturn.Data = usersList;
            oReturn.Message = $"Se encontraron {usersList.Count} registros de usuarios en el sistema.";
            oReturn.Status = (int)ServiceStatusCode.Success;

            return oReturn;
        }
        #endregion

        #region Create User
        /// <summary>
        /// Create or Update user
        /// </summary>
        /// <param name="input">UserDto</param>
        /// <returns> List<UserDto></returns>
        public async Task<GeneralResponse> CreateUser(UserRequest userIn)
        {
            var oReturn = new GeneralResponse();
            var users = new List<UserModel>();

            var user = await _UserRepositorie.GetUserByEmailAsync(userIn.EmailIn);

            if (user == null)
            {
               var userDb = _UserRepositorie.CreateAsync(new UserRequest()
                {
                  Name = userIn.Name,
                  EmailIn = userIn.EmailIn,
                 });

               oReturn.Data = true;
               oReturn.Status = (int)ServiceStatusCode.Success;
               oReturn.Message = "El usuario se creo exitosamente en el sistema.";
            }
            else
            {
              oReturn.Message = "El usuario ya se encuentra registrado en el sistema.";
              oReturn.Data = false;
              oReturn.Status = (int)ServiceStatusCode.ValidationError;
            }
            return oReturn;
        }
        #endregion

    }
}
