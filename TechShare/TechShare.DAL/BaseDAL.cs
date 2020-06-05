using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Text;

namespace TechShare.DAL
{
    public class BaseDAL : IBaseDAL
    {
        private readonly string _connectionString = "";

        private SqlConnection _sqlConnection;
        private SqlCommand _sqlCommand;
        private SqlTransaction _sqlTransaction;

        public BaseDAL(string connectionString)
        {
            _connectionString = connectionString;
            _sqlConnection = new SqlConnection(_connectionString);
            _sqlCommand = _sqlConnection.CreateCommand();
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlConnection.Open();
        }

        /// <summary>
        /// Thực hiện gán tham số vào procedure, thực thi và đọc dữ liệu
        /// </summary>                     
        public IEnumerable<object> ExecuteReader(string procName, object param)
        {
            if (_sqlConnection.State != ConnectionState.Open) _sqlConnection.Open();

            _sqlCommand.CommandText = procName;
            if (param != null) AddParam(param);           
            SqlDataReader sqlDataReader = _sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                dynamic obj = new ExpandoObject { };
                for (int i = 0; i < sqlDataReader.FieldCount; i++)
                {
                    var objDict = obj as IDictionary<string, object>;
                    var fieldName = sqlDataReader.GetName(i);
                    var fieldValue = sqlDataReader.GetValue(i);
                    objDict.Add(fieldName, fieldValue);                    
                }
                yield return obj;
            }
            sqlDataReader.Close();
            _sqlConnection.Close();
        }

        /// <summary>
        /// Thực hiện gán tham số vào procedure và thực thi
        /// </summary>            
        public int ExecuteNonQuery(string procName, object param)
        {
            _sqlTransaction = _sqlConnection.BeginTransaction();
            _sqlCommand.Transaction = _sqlTransaction;
            _sqlCommand.CommandText = procName;
            if (param != null) AddParam(param);

            var result = _sqlCommand.ExecuteNonQuery();
            _sqlTransaction.Commit();
            return result;
        }

        /// <summary>
        /// Thực hiện gán tham số vào procedure
        /// </summary>       
        public void AddParam(object param)
        {
            SqlCommandBuilder.DeriveParameters(_sqlCommand);
            var parameters = _sqlCommand.Parameters;

            for (int i = 1; i < parameters.Count; i++)
            {
                var paramName = parameters[i].ParameterName;
                paramName = paramName.Replace("@", "");

                var property = param.GetType().GetProperty(paramName);
                parameters[i].Value = property != null ? property.GetValue(param) : param;
            }
        }

    }
}
