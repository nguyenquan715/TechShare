using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechShare.Models
{
    public class ResponseModel
    {       
        public List<MessageModel> Response { get; set; }

        public ResponseModel() {
            this.Response = new List<MessageModel>();
        }
        public ResponseModel(List<MessageModel> res)
        {
            this.Response = res;
        }
        public ResponseModel(string code,string mess)
        {
            this.Response = new List<MessageModel>();
            this.AddError(code, mess);
        }
        public void AddError(string code,string mess)
        {
            var res = new MessageModel
            {
                Code = code,
                Message = mess
            };
            this.Response.Add(res);          
        }
    }
}
