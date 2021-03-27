using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MasudaManager.DataAccess;

namespace MasudaManager.Utility
{
    class SqlInputAssistantDbCommandBuilder : DbCommandBuilder
    {
        const string SCHEMA_NAME = "SCHEMA_NAME";
        const string TABLE_NAME = "TABLE_NAME";

        public SqlInputAssistantComplementMode ComplementMode { get; set; }

        public override DbCommandSet CreateCommand(string value)
        {
            switch (this.ComplementMode)
            {
                case SqlInputAssistantComplementMode.ObjectName:
                    return GetObjectNameCommand(value.ToUpper());
                case SqlInputAssistantComplementMode.ColumnName:
                    return GetColumnNameCommand(value.ToUpper());
                case SqlInputAssistantComplementMode.InlineViewColumn:
                    return base.CreateCommand(value);
            }

            throw new Exception("Invalid SqlInputAssistantComplementMode");
        }


        DbCommandSet GetObjectNameCommand(string schemaName)
        {
           string sql = this.DataAccess.SqlLibrary.SqlUserHelpTableInfo;          
           return CreateCommand(sql, SCHEMA_NAME, schemaName);
        }

        DbCommandSet GetColumnNameCommand(string tableName)
        {
            string sql = this.DataAccess.SqlLibrary.SqlUserHelpColumnInfo;
            return CreateCommand(sql, GetColumnNameParameterNames(), GetColumnNameParameterValues(tableName));
        }

        IEnumerable<string> GetColumnNameParameterNames()
        {
            yield return SCHEMA_NAME;
            yield return TABLE_NAME;
        }

        IEnumerable<object> GetColumnNameParameterValues(string tableName)
        {
            yield return this.DataAccess.CurrentConnectionData.UserId.ToUpper();
            yield return tableName.ToUpper();
        }
    }
}
