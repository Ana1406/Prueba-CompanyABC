using Backend.DataBase.DataBase;
using Backend.Domain.Enums;
using Backend.Domain.Models;
using Backend.Domain.Response;
using Microsoft.Extensions.Configuration;


namespace Backend.Core.Core
{
    
    public class HealthCore : IHealthCore
    {
        private readonly DbContext _dbContext;
        private readonly IConfiguration _configuration;
        private string NameService = "";
        public HealthCore(DbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            NameService = _configuration["NameMicroServices"];
        }

        #region Get health Information
        /// <summary>
        /// Get health Information
        /// </summary>
        /// <returns>HealthResponse </returns>
        public async Task<GeneralResponse> HealthAsync()
        {
            var oReturn = new GeneralResponse();

                var mongoOk = await _dbContext.CheckConnectionAsync();
                if (!mongoOk) {
                    oReturn.Data = new HealthResponse()
                    {
                        Service = NameService,
                        DataBase = "MongoDb Desconectado",
                        TimeStamp = DateTime.UtcNow,
                    };
                    oReturn.Message = "Servicio Presenta falla al conectar MongoDB";
                    oReturn.Status = (int)ServiceStatusCode.InternalError;

                }
                oReturn.Data = new HealthResponse()
                {
                    Service = NameService,
                    DataBase = "MongoDb Conectado",
                    TimeStamp = DateTime.UtcNow,
                };
                oReturn.Message = "Servicio Funcionando Correctamente";
                oReturn.Status = (int)ServiceStatusCode.Success;


            return oReturn;
        }
        #endregion

        #region Get Status Info
        /// <summary>
        /// Get Status Info
        /// </summary>
        /// <returns> StatusResponse</returns>
        public async Task<GeneralResponse> StatusAsync()
        {
            var oReturn = new GeneralResponse();

                var version = _configuration["Version"];
                var status = new StatusResponse()
                {
                    Service= NameService,
                    Version= version,
                    Environment= Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                    TimeStamp= DateTime.UtcNow,

                };

                    oReturn.Data = status;
                    oReturn.Message = $"Estado del servicio {NameService}";
                oReturn.Status = (int)ServiceStatusCode.Success;
            return oReturn;
        }
        #endregion

    }
}
