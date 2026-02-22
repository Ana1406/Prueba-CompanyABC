
namespace Backend.Domain.Models
{
    public class Enums { 
        public enum ServiceStatusCode
        {
            Success = 200,
            ServiceUnavailable = 503,
            InternalError = 500,
            ValidationError = 400,
            Unauthorized = 401
        }
    }
}
