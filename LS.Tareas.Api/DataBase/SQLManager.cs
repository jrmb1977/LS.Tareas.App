using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LS.Tareas.Api.DataBase
{
    public class SQLManager
    {
        #region Stored Procedures Names Properties

        public string SPSelect { get; set; }
        public string SPSelectRow { get; set; }
        public string SPInsert { get; set; }
        public string SPUpdate { get; set; }
        public string SPUpdateStatus { get; set; }
        public string SPDelete { get; set; }

        #endregion

        #region SQL Command Properties

        public string sqlSelect { get; set; }
        public string sqlSelectShort { get; set; }
        public string sqlSelectGroupBy { get; set; }
        public string sqlSelectChildren { get; set; }
        public string sqlSelectChildrenShort { get; set; }
        public string sqlSelectParent { get; set; }
        public string sqlSelectParentShort { get; set; }
        public string sqlInsert { get; set; }
        public string sqlUpdate { get; set; }
        public string sqlSetStatus { get; set; }
        public string sqlDelete { get; set; }
        public string sqlFilter { get; set; }
        public string sqlFilterUnique { get; set; }
        public string sqlFilterType { get; set; }
        public string sqlFilterStatus { get; set; }
        public string sqlFilterParent { get; set; }
        public string sqlSet { get; set; }
        public string sqlValues { get; set; }

        #endregion

        #region Dictionary Params Properties

        public Dictionary<string, object> paramsSelect { get; set; }
        public Dictionary<string, object> paramsSelectUnique { get; set; }
        public Dictionary<string, object> paramsSelectType { get; set; }
        public Dictionary<string, object> paramsSelectParent { get; set; }
        public Dictionary<string, object> paramsInsert { get; set; }
        public Dictionary<string, object> paramsUpdate { get; set; }
        public Dictionary<string, object> paramsDelete { get; set; }

        #endregion

        #region SELECT FROM Table

        public string SQLSelect => new StringBuilder(sqlSelect).ToString();//SELECT * FROM Table
        public string SQLSelectShort => new StringBuilder(sqlSelectShort).ToString();//SELECT Id, Name FROM Table
        public string SQLSelectItemCount => new StringBuilder(sqlSelectGroupBy).ToString();//SELECT ParentId, COUNT(*) FROM Table GROUP BY ParentId
        public string SQLSelectChildren => new StringBuilder(sqlSelectChildren).ToString();// SELECT * FROM ChildTable
        public string SQLSelectChildrenShort => new StringBuilder(sqlSelectChildrenShort).ToString();// SELECT Id, Name FROM ChildTable

        #endregion

        #region SELECT FROM Table WHERE Filter

        #region Table

        public string SQLSelectWhere => new StringBuilder(sqlSelect).Append(sqlFilter).ToString();
        public string SQLSelectShortWhere => new StringBuilder(sqlSelectShort).Append(sqlFilter).ToString();
        public string SQLSelectWhereUnique => new StringBuilder(sqlSelect).Append(sqlFilterUnique).ToString();
        public string SQLSelectWhereType => new StringBuilder(sqlSelect).Append(sqlFilterType).ToString();
        public string SQLSelectShortWhereType => new StringBuilder(sqlSelect).Append(sqlFilterType).ToString();
        public string SQLSelectWhereStatus => new StringBuilder(sqlSelect).Append(sqlFilterStatus).ToString();
        public string SQLSelectWhereParent => new StringBuilder(sqlSelect).Append(sqlFilterParent).ToString();
        public string SQLSelectShortWhereParent => new StringBuilder(sqlSelectShort).Append(sqlFilterParent).ToString();

        #endregion

        #region Child Table

        public string SQLSelectChildrenWhere => new StringBuilder(sqlSelectChildren).Append(sqlFilter).ToString();
        public string SQLSelectChildrenShortWhere => new StringBuilder(sqlSelectChildrenShort).Append(sqlFilter).ToString();

        #endregion

        #region Parent Table

        public string SQLSelectParentWhere => new StringBuilder(sqlSelectParent).Append(sqlFilterParent).ToString();
        public string SQLSelectParentShortWhere => new StringBuilder(sqlSelectParentShort).Append(sqlFilterParent).ToString();

        #endregion

        #endregion

        #region Insert, Update, Delete

        public string SQLInsert => new StringBuilder(sqlInsert).Append(sqlValues).ToString();
        public string SQLUpdate => new StringBuilder(sqlUpdate).Append(sqlSet).Append(sqlFilter).ToString();
        public string SQLUpdateStatus => new StringBuilder(sqlUpdate).Append(sqlSetStatus).Append(sqlFilter).ToString();
        public string SQLDelete => new StringBuilder(sqlDelete).Append(sqlFilter).ToString();

        #endregion

        public string GetStoredProcedures()
        {
            StringBuilder builder = new StringBuilder();

            #region Select

            builder.AppendLine(String.Format("CREATE PROCEDURE [dbo].[{0}]", SPSelect));
            builder.AppendLine("AS");
            builder.AppendLine("BEGIN");
            builder.AppendLine("SET NOCOUNT ON");
            builder.AppendLine(SQLSelectShort);
            builder.AppendLine("END");
            builder.AppendLine();

            #endregion

            #region Select Row

            builder.AppendLine(String.Format("CREATE PROCEDURE [dbo].[{0}](", SPSelectRow));
            foreach (var kp in paramsSelect)
            {
                builder.AppendLine(String.Format("{0},", kp.Key));
            }
            builder.AppendLine(")");
            builder.AppendLine("AS");
            builder.AppendLine("BEGIN");
            builder.AppendLine("SET NOCOUNT ON");
            builder.AppendLine(SQLSelectWhere);
            builder.AppendLine("END");
            builder.AppendLine();

            #endregion

            #region Insert

            builder.AppendLine(String.Format("CREATE PROCEDURE [dbo].[{0}](", SPInsert));
            foreach (var kp in paramsInsert)
            {
                builder.AppendLine(String.Format("{0},", kp.Key));
            }
            builder.AppendLine(")");
            builder.AppendLine("AS");
            builder.AppendLine("BEGIN");
            builder.AppendLine(SQLInsert);
            builder.AppendLine("END");
            builder.AppendLine();

            #endregion

            #region Update

            builder.AppendLine(String.Format("CREATE PROCEDURE [dbo].[{0}](", SPUpdate));
            foreach (var kp in paramsUpdate)
            {
                builder.AppendLine(String.Format("{0},", kp.Key));
            }
            builder.AppendLine(")");
            builder.AppendLine("AS");
            builder.AppendLine("BEGIN");
            builder.AppendLine(SQLUpdate);
            builder.AppendLine("END");
            builder.AppendLine();

            #endregion

            #region Update Status

            builder.AppendLine(String.Format("CREATE PROCEDURE [dbo].[{0}](", SPUpdateStatus));
            foreach (var kp in paramsUpdate)
            {
                builder.AppendLine(String.Format("{0},", kp.Key));
            }
            builder.AppendLine(")");
            builder.AppendLine("AS");
            builder.AppendLine("BEGIN");
            builder.AppendLine(SQLUpdateStatus);
            builder.AppendLine("END");
            builder.AppendLine();

            #endregion

            #region Delete

            builder.AppendLine(String.Format("CREATE PROCEDURE [dbo].[{0}](", SPDelete));
            foreach (var kp in paramsDelete)
            {
                builder.AppendLine(String.Format("{0},", kp.Key));
            }
            builder.AppendLine(")");
            builder.AppendLine("AS");
            builder.AppendLine("BEGIN");
            builder.AppendLine(SQLDelete);
            builder.AppendLine("END");
            builder.AppendLine();

            #endregion

            string sql = builder.ToString();
            return sql;
        }
    }
}