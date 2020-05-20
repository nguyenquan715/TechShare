using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechShare.Infra
{
    public static class ErrorConstant
    {        
        public const string SucceedCode = "Success";
        public const string NoContentCode = "NoContent";
        public const string InvalidModelCode = "InvalidModel";       
        public const string UserBlockedCode = "UserBlocked";
        public const string NotInRoleCode = "NotInRole";

        public const string SucceedMess = "Thành công";
        public const string NoContentMess = "Không tìm thấy dữ liệu";
        public const string InvalidModelMess = "Model Invalid";        
        public const string UserBlockedMess = "Người dùng đã bị chặn";
        public const string NotInRoleMess = "Người dùng không có ca sử dụng này";
    }
}
