using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechShare.Models
{
    public class ResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ResponseModel(int code,string mess)
        {
            StatusCode = code;
            Message = mess;
        }
    }
}
