using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechShare.Models
{
    public class ResponseModel
    {       
        public List<PayloadModel> Response { get; set; }

        public ResponseModel() {
            this.Response = new List<PayloadModel>();
        }
        public ResponseModel(List<PayloadModel> res)
        {
            this.Response = res;
        }
        public ResponseModel(string code,string mess)
        {
            this.Response = new List<PayloadModel>();
            this.AddError(code, mess);
        }
        public void AddError(string code,string mess)
        {
            var res = new PayloadModel
            {
                Code = code,
                Message = mess
            };
            this.Response.Add(res);          
        }
    }
}
