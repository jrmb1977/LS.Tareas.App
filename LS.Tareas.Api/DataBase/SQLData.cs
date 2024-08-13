using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace LS.Tareas.Api.DataBase
{
    public class SQLData : IDataBase
    {
        private SqlConnection _connection;
        private SqlCommand _command;

        public SQLData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _connection = new SqlConnection(connectionString);
            _command = _connection.CreateCommand();
            _command.CommandType = CommandType.Text;
        }

        public void Open()
        {
            if (_connection.State.Equals(ConnectionState.Closed))
                _connection.Open();
        }

        public void Close()
        {
            if (_connection.State.Equals(ConnectionState.Open))
                _connection.Close();
        }

        public void ClearParameters()
        {
            _command.Parameters.Clear();
        }

        public void AddParameter(string parameterName)
        {
            _command.Parameters.AddWithValue(parameterName, DBNull.Value);
        }

        public void AddParameter(string parameterName, object value)
        {
            _command.Parameters.AddWithValue(parameterName, value);
        }

        public int ExecuteNonQuery(string sqlNonQuery)
        {
            _command.CommandType = CommandType.Text;
            _command.CommandText = sqlNonQuery;
            int numberOfRecordsAffected = _command.ExecuteNonQuery();
            return numberOfRecordsAffected;
        }

        public int ExecuteNonQueryProcedure(string procedureName)
        {
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = procedureName;
            int numberOfRecordsAffected = _command.ExecuteNonQuery();
            return numberOfRecordsAffected;
        }

        public DataSet ExecuteQuery(string sqlQuery)
        {
            _command.CommandType = CommandType.Text;
            _command.CommandText = sqlQuery;
            DataSet ds = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter(_command))
            {
                adapter.Fill(ds);
            }
            return ds;
        }

        public DataSet ExecuteQueryProcedure(string procedureName)
        {
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = procedureName;
            DataSet ds = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter(_command))
            {
                adapter.Fill(ds);
            }
            return ds;
        }

        public DataTable ExecuteQueryToTable(string sqlQuery)
        {
            DataTable dt = new DataTable();
            DataSet ds = ExecuteQuery(sqlQuery);
            if (ds.Tables.Count > 0)
                dt = ds.Tables[0];
            return dt;
        }

        public DataTable ExecuteQueryProcedureToTable(string procedureName)
        {
            DataTable dt = new DataTable();
            DataSet ds = ExecuteQueryProcedure(procedureName);
            if (ds.Tables.Count > 0)
                dt = ds.Tables[0];
            return dt;
        }
    }
}