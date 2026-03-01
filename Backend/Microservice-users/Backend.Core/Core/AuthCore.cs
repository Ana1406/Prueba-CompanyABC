using Backend.Core.Core.Interfaces;
using Backend.DataBase.Repositories;
using Backend.Domain.Enums;
using Backend.Domain.Models;
using Backend.Domain.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Backend.Core.Core
{
    
    public class AuthCore : IAuthCore
    {
        private readonly IUserRepositorie _UserRepositorie;
        private readonly IConfiguration _configuration;

        public AuthCore(IUserRepositorie userRepositorie, IConfiguration configuration)
        {
            _UserRepositorie = userRepositorie;
            _configuration = configuration;
        }


        #region Login
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login">LoginRequest</param>
        /// <returns>string</returns>
        public async Task<GeneralResponse<string>> Login(LoginRequest login)
        {
            var oReturn = new GeneralResponse<string>();

            var user = await _UserRepositorie.GetUserByEmailAsync(login.Email);

            bool isValid = BCrypt.Net.BCrypt.Verify(login.Password, user.Password);

            if (isValid)
            {
               var token = await GenerateToken(user);

               oReturn.Data = token;
               oReturn.Status = (int)ServiceStatusCode.Success;
               oReturn.Message = $"El usuario inicio sersion de manera correcta";
            }
            else
            {
              oReturn.Message = "La contraseña no coindice.";
              oReturn.Data = null;
              oReturn.Status = (int)ServiceStatusCode.ValidationError;
            }
            return oReturn;
        }
        #endregion

        private async Task<string> GenerateToken(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id),
                new Claim("Email", user.Email),
                new Claim("Name", user.Name),
                new Claim("Rol", user.Rol)
            };
            var keyBytes = Convert.FromBase64String(_configuration["Jwt:Key"]);

            var key = new SymmetricSecurityKey(keyBytes);

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(_configuration["Jwt:ExpireMinutes"])
                ),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       
    }
}
