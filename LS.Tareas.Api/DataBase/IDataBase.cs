using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Tareas.Api.DataBase
{
    public interface IDataBase
    {
        void Open();
        void Close();
        void ClearParameters();
        void AddParameter(string parameterName);
        void AddParameter(string parameterName, object value);
        int ExecuteNonQuery(string sqlNonQuery);
        int ExecuteNonQueryProcedure(string procedureName);
        DataSet ExecuteQuery(string sqlQuery);
        DataSet ExecuteQueryProcedure(string procedureName);
        DataTable ExecuteQueryToTable(string sqlQuery);
        DataTable ExecuteQueryProcedureToTable(string procedureName);
    }
}
