using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using LS.Tareas.Api.DataBase;

namespace LS.Tareas.Api.Models
{
    public class Entidad
    {
        public Entidad(DataRow row = null)
        {
            SetEntidad(row);
        }

        private const string BasicTypesList = "String,Int32,Int64,Decimal,DateTime,Boolean";

        private static string[] BasicTypes = BasicTypesList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        public void SetEntidad(DataRow row = null)
        {
            if (row is null)
                return;
            var type = this.GetType();
            var fields = type.GetFields();
            var properties = type.GetProperties();
            var columnas = row.Table.Columns;
            FieldInfo[] fieldInfo = fields.Where(f => BasicTypes.Contains(f.FieldType.Name) && columnas.Contains(f.Name)).ToArray();
            PropertyInfo[] propertyInfo = properties.Where(p => BasicTypes.Contains(p.PropertyType.Name) && columnas.Contains(p.Name)).ToArray();
            foreach (var field in fieldInfo)
            {
                string fieldName = field.Name;
                Type fieldType = field.FieldType;
                object value = GetResultado(row, fieldType, fieldName);
                field.SetValue(this, value);
            }

            foreach (var property in propertyInfo)
            {
                string propertyName = property.Name;
                Type propertyType = property.PropertyType;
                object value = GetResultado(row, propertyType, propertyName);
                if (value != null)
                    property.SetValue(this, value);
            }
        }

        private static object GetResultado(DataRow row, Type type, string name)
        {
            var columnas = row.Table.Columns;
            if (row[name] != null)
            {
                if (row[name] != DBNull.Value)
                {
                    if (type == Type.GetType("System.String"))
                    {
                        return row.ParseString(name);
                    }
                    if (type == Type.GetType("System.Int32"))
                    {
                        return row.ParseInteger(name);
                    }
                    if (type == Type.GetType("System.Int64"))
                    {
                        long longValue = 0;
                        long.TryParse(row[name].ToString(), out longValue);
                        return longValue;
                    }
                    if (type == Type.GetType("System.Decimal"))
                    {
                        decimal decimalValue = 0;
                        decimal.TryParse(row[name].ToString(), out decimalValue);
                        return decimalValue;
                    }
                    if (type == Type.GetType("System.DateTime"))
                    {
                        return row.ParseDateTime(name);
                    }
                    if (type == Type.GetType("System.Boolean"))
                    {
                        return row.ParseBoolean(name);
                    }
                }
            }
            return null;
        }
    }
}