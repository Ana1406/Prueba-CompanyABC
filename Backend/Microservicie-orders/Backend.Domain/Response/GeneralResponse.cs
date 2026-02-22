using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Backend.Domain.Models.Enums;

namespace Backend.Domain.Response
{
    public class GeneralResponse
    {

        public GeneralResponse()
        {

        }
        public GeneralResponse(string message, dynamic data, int status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
        public int Status { get; set; } =(int)ServiceStatusCode.Success;
        public dynamic? Data { get; set; }
        public string Message { get; set; }
    };
}
