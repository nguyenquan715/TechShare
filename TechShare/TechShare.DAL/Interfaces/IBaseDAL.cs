using System;
using System.Collections.Generic;
using System.Text;

namespace TechShare.DAL
{
    /*Sử dụng ADO.NET để trích xuất dữ liệu từ DB*/
    public interface IBaseDAL
    {
        IEnumerable<object> ExecuteReader(string procName, object param);
        int ExecuteNonQuery(string procName, object param);
    }
}
