using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LS.Tareas.Api.DataBase
{
    public static class DataRowExtensions
    {
        public static int ParseInteger(this DataRow row, string columnName)
        {
            int resultado = 0;
            string str = ParseString(row, columnName);
            if (!string.IsNullOrEmpty(str))
                int.TryParse(str, out resultado);
            return resultado;
        }

        public static string ParseString(this DataRow row, string columnName)
        {
            string resultado = "";
            if (row[columnName] != null)
                if (row[columnName] != DBNull.Value)
                    resultado = row[columnName].ToString();
            return resultado;
        }

        public static bool ParseBoolean(this DataRow row, string columnName)
        {
            string resultado = ParseString(row, columnName);
            return resultado.ToLower().Equals("true");
        }

        public static DateTime ParseDateTime(this DataRow row, string columnName)
        {
            DateTime resultado = new DateTime(1900, 1, 1);
            if (row[columnName] != null)
                if (row[columnName] != DBNull.Value)
                    resultado = (DateTime)row[columnName];
            return resultado;
        }
    }
}