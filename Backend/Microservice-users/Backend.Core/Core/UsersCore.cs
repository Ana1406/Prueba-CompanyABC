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
        public async Task<GeneralResponse<List<UserResponse>>> GetAllUsers(FilterUsersRequest input)
        {
            var oReturn = new GeneralResponse<List<UserResponse>> ();

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
        public async Task<GeneralResponse<string>> CreateUser(UserRequest userIn)
        {
            var oReturn = new GeneralResponse<string>();
            var users = new List<UserModel>();

            var user = await _UserRepositorie.GetUserByEmailAsync(userIn.EmailIn);

            if (user == null)
            {
               //Encriptacion o Hasheo de contraseña
               userIn.Password = BCrypt.Net.BCrypt.HashPassword(userIn.Password);
               var userDb = await _UserRepositorie.CreateAsync(userIn);

               oReturn.Data = userDb;
               oReturn.Status = (int)ServiceStatusCode.Success;
               oReturn.Message = $"El usuario se creo exitosamente en el sistema, con el id {userDb}";
            }
            else
            {
              oReturn.Message = "El usuario ya se encuentra registrado en el sistema.";
              oReturn.Data = null;
              oReturn.Status = (int)ServiceStatusCode.ValidationError;
            }
            return oReturn;
        }
        #endregion

        #region Get userId by email
        /// <summary>
        /// Get userId by email
        /// </summary>
        /// <returns>List<UserDto> </returns>
        public async Task<GeneralResponse<string>> GetUserIdByEmail(string email)
        {
            var oReturn = new GeneralResponse<string>();

            var user = await _UserRepositorie.GetUserByEmailAsync(email);

            if (user == null)
            {
                oReturn.Data = null;
                oReturn.Message = $" No se encontro para el email {email} el correspondiente id en el sistema";
                oReturn.Status = (int)ServiceStatusCode.ValidationError;
            }

            oReturn.Data = user.Id;
            oReturn.Message = $"Se encontro para el email {email} el correspondiente id en el sistema";
            oReturn.Status = (int)ServiceStatusCode.Success;

            return oReturn;
        }
        #endregion
    }
}
