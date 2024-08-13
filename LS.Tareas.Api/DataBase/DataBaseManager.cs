using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LS.Tareas.Api.DataBase
{
    public class DataBaseManager
    {
        private readonly IDataBase _database;
        private SQLManager _sqlManager;

        public DataBaseManager(IDataBase dataBase, SQLManager sqlManager)
        {
            _database = dataBase;
            _sqlManager = sqlManager;
        }

        public void Refresh(SQLManager sqlManager)
        {
            _sqlManager = sqlManager;
        }

        public void Open()
        {
            _database.Open();
        }

        public void Close()
        {
            _database.Close();
        }

        #region Select (Short|All) All | Select (Short|All) By Id

        public DataTable Select(bool UseProcedure = false)// SELECT * FROM Table
        {
            _database.ClearParameters();
            if (UseProcedure)
            {
                string procedureName = _sqlManager.SPSelect;
                DataTable dt = _database.ExecuteQueryProcedureToTable(procedureName);
                return dt;
            }
            else
            {
                string sqlQuery = _sqlManager.SQLSelect;
                DataTable dt = _database.ExecuteQueryToTable(sqlQuery);
                return dt;
            }
        }

        public DataTable SelectShort(bool UseProcedure = false)// SELECT Id, Name FROM Table
        {
            _database.ClearParameters();
            if (UseProcedure)
            {
                string procedureName = _sqlManager.SPSelect;
                DataTable dt = _database.ExecuteQueryProcedureToTable(procedureName);
                return dt;
            }
            else
            {
                string sqlQuery = _sqlManager.SQLSelectShort;
                DataTable dt = _database.ExecuteQueryToTable(sqlQuery);
                return dt;
            }
        }

        public DataTable SelectWhere(bool UseProcedure = false)// SELECT * FROM Table WHERE PK = @PK
        {
            var paramsSelect = _sqlManager.paramsSelect;
            _database.ClearParameters();
            foreach (var p in paramsSelect)
            {
                _database.AddParameter(p.Key, p.Value);
            }

            if (UseProcedure)
            {
                string procedureName = _sqlManager.SPSelectRow;
                DataTable dt = _database.ExecuteQueryProcedureToTable(procedureName);
                return dt;
            }
            else
            {
                string sqlQuery = _sqlManager.SQLSelectWhere;
                DataTable dt = _database.ExecuteQueryToTable(sqlQuery);
                return dt;
            }
        }

        public DataTable SelectShortWhere(bool UseProcedure = false)// SELECT Id, Name FROM Table WHERE PK = @PK
        {
            var paramsSelect = _sqlManager.paramsSelect;
            _database.ClearParameters();
            foreach (var p in paramsSelect)
            {
                _database.AddParameter(p.Key, p.Value);
            }
            if (UseProcedure)
            {
                string procedureName = _sqlManager.SPSelectRow;
                DataTable dt = _database.ExecuteQueryProcedureToTable(procedureName);
                return dt;
            }
            else
            {
                string sqlQuery = _sqlManager.SQLSelectShortWhere;
                DataTable dt = _database.ExecuteQueryToTable(sqlQuery);
                return dt;
            }
        }

        #endregion

        public DataTable SelectWhereUnique()// SELECT * FROM Table WHERE UK = @UK
        {
            string sqlQuery = _sqlManager.SQLSelectWhereUnique;
            var paramsSelect = _sqlManager.paramsSelectUnique;
            _database.ClearParameters();
            foreach (var p in paramsSelect)
            {
                _database.AddParameter(p.Key, p.Value);
            }
            DataTable dt = _database.ExecuteQueryToTable(sqlQuery);
            return dt;
        }

        public DataTable SelectWhereType()// SELECT * FROM Table WHERE Type = @Type
        {
            string sqlQuery = _sqlManager.SQLSelectWhereType;
            var paramsSelect = _sqlManager.paramsSelectType;
            _database.ClearParameters();
            foreach (var p in paramsSelect)
            {
                _database.AddParameter(p.Key, p.Value);
            }
            DataTable dt = _database.ExecuteQueryToTable(sqlQuery);
            return dt;
        }

        public DataTable SelectShortWhereType()// SELECT * FROM Table WHERE Type = @Type
        {
            string sqlQuery = _sqlManager.SQLSelectShortWhereType;
            var paramsSelect = _sqlManager.paramsSelectType;
            _database.ClearParameters();
            foreach (var p in paramsSelect)
            {
                _database.AddParameter(p.Key, p.Value);
            }
            DataTable dt = _database.ExecuteQueryToTable(sqlQuery);
            return dt;
        }

        public DataTable SelectWhereParent()// SELECT * FROM Table WHERE FK = @PK
        {
            string sqlQuery = _sqlManager.SQLSelectWhereParent;
            var paramsSelect = _sqlManager.paramsSelectParent;
            _database.ClearParameters();
            foreach (var p in paramsSelect)
            {
                _database.AddParameter(p.Key, p.Value);
            }
            DataTable dt = _database.ExecuteQueryToTable(sqlQuery);
            return dt;
        }

        public DataTable SelectShortWhereParent()// SELECT * FROM Table WHERE FK = @PK
        {
            string sqlQuery = _sqlManager.SQLSelectShortWhereParent;
            var paramsSelect = _sqlManager.paramsSelectParent;
            _database.ClearParameters();
            foreach (var p in paramsSelect)
            {
                _database.AddParameter(p.Key, p.Value);
            }
            DataTable dt = _database.ExecuteQueryToTable(sqlQuery);
            return dt;
        }

        public DataTable SelectChildren()// SELECT * FROM ChildTable
        {
            string sqlQuery = _sqlManager.SQLSelectChildren;
            _database.ClearParameters();
            DataTable dt = _database.ExecuteQueryToTable(sqlQuery);
            return dt;
        }

        public DataTable SelectChildrenShort()// SELECT Id, Name FROM ChildTable
        {
            string sqlQuery = _sqlManager.SQLSelectShortWhereParent;
            _database.ClearParameters();
            DataTable dt = _database.ExecuteQueryToTable(sqlQuery);
            return dt;
        }

        public DataTable SelectChildrenWhere()// SELECT * FROM ChildTable WHERE FK = @PK
        {
            string sqlQuery = _sqlManager.SQLSelectChildrenWhere;
            var paramsSelect = _sqlManager.paramsSelect;
            _database.ClearParameters();
            foreach (var p in paramsSelect)
            {
                _database.AddParameter(p.Key, p.Value);
            }
            DataTable dt = _database.ExecuteQueryToTable(sqlQuery);
            return dt;
        }

        public DataTable SelectParentWhere()
        {
            string sqlQuery = _sqlManager.SQLSelectParentWhere;
            var paramsSelect = _sqlManager.paramsSelectParent;
            _database.ClearParameters();
            foreach (var p in paramsSelect)
            {
                _database.AddParameter(p.Key, p.Value);
            }
            DataTable dt = _database.ExecuteQueryToTable(sqlQuery);
            return dt;
        }

        public DataTable SelectParentShortWhere()
        {
            string sqlQuery = _sqlManager.SQLSelectParentShortWhere;
            var paramsSelect = _sqlManager.paramsSelectParent;
            _database.ClearParameters();
            foreach (var p in paramsSelect)
            {
                _database.AddParameter(p.Key, p.Value);
            }
            DataTable dt = _database.ExecuteQueryToTable(sqlQuery);
            return dt;
        }

        public int UpdateWhere(bool UseProcedure = false)// UPDATE Table SET ... WHERE PK = @PK
        {
            int numberOfRowsAffected = 0;
            var paramsUpdate = _sqlManager.paramsUpdate;
            _database.ClearParameters();
            foreach (var p in paramsUpdate)
            {
                _database.AddParameter(p.Key, p.Value);
            }
            if (UseProcedure)
            {
                string procedureName = _sqlManager.SPUpdate;
                numberOfRowsAffected = _database.ExecuteNonQueryProcedure(procedureName);
            }
            else
            {
                string sqlNonQuery = _sqlManager.SQLUpdate;
                numberOfRowsAffected = _database.ExecuteNonQuery(sqlNonQuery);
            }
            return numberOfRowsAffected;
        }

        public int Insert(bool UseProcedure = false)// INSERT INTO Table VALUES
        {
            int numberOfRowsAffected = 0;
            var paramsInsert = _sqlManager.paramsInsert;
            _database.ClearParameters();
            foreach (var p in paramsInsert)
            {
                _database.AddParameter(p.Key, p.Value);
            }
            if (UseProcedure)
            {
                string procedureName = _sqlManager.SPInsert;
                numberOfRowsAffected = _database.ExecuteNonQueryProcedure(procedureName);
            }
            else
            {
                string sqlNonQuery = _sqlManager.SQLInsert;
                numberOfRowsAffected = _database.ExecuteNonQuery(sqlNonQuery);
            }
            return numberOfRowsAffected;
        }

        public int UpdateStatusWhere(bool UseProcedure = false)// UPDATE Table SET ... WHERE PK = @PK
        {
            int numberOfRowsAffected = 0;
            var paramsUpdate = _sqlManager.paramsSelect;
            _database.ClearParameters();
            foreach (var p in paramsUpdate)
            {
                _database.AddParameter(p.Key, p.Value);
            }
            if (UseProcedure)
            {
                string procedureName = _sqlManager.SPUpdateStatus;
                numberOfRowsAffected = _database.ExecuteNonQueryProcedure(procedureName);
            }
            else
            {
                string sqlNonQuery = _sqlManager.SQLUpdateStatus;
                numberOfRowsAffected = _database.ExecuteNonQuery(sqlNonQuery);
            }
            return numberOfRowsAffected;
        }

        public int DeleteWhere(bool UseProcedure = false)// DELETE FROM Table ... WHERE PK = @PK
        {
            int numberOfRowsAffected = 0;
            var paramsUpdate = _sqlManager.paramsSelect;
            _database.ClearParameters();
            foreach (var p in paramsUpdate)
            {
                _database.AddParameter(p.Key, p.Value);
            }
            if (UseProcedure)
            {
                string procedureName = _sqlManager.SPDelete;
                numberOfRowsAffected = _database.ExecuteNonQueryProcedure(procedureName);
            }
            else
            {
                string sqlNonQuery = _sqlManager.SQLDelete;
                numberOfRowsAffected = _database.ExecuteNonQuery(sqlNonQuery);
            }
            return numberOfRowsAffected;
        }

        public DataTable SelectItemCount()
        {
            string sqlQuery = _sqlManager.SQLSelectItemCount;
            _database.ClearParameters();
            DataTable dt = _database.ExecuteQueryToTable(sqlQuery);
            return dt;
        }
    }
}