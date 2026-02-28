using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Response
{
    public class GeneralResponse<T>
    {

        public GeneralResponse()
        {

        }
        public GeneralResponse(string message, T data, int status)
        {
            Message = message;
            Data = data;
            Status = status;
        }
        public int Status { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; }
    };
}
