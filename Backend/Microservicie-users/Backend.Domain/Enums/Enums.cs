using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Enums
{
    public enum ServiceStatusCode
    {
        Success = 200,
        ServiceUnavailable = 503,
        InternalError = 500,
        ValidationError = 400,
        Unauthorized = 401
    }
}
