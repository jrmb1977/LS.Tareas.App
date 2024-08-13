using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.Tareas.Api.DataBase
{
    public class TableManager
    {
        public DataBaseManager _dataBaseManager;

        public TableManager(SQLManager sqlManager)
        {
            IDataBase _dataBase = new SQLData();
            _dataBaseManager = new DataBaseManager(_dataBase, sqlManager);
        }
    }
}