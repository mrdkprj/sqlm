using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MasudaManager.DataAccess;
using MasudaManager.Utility.Preference;

namespace MasudaManager.Utility
{
    public class DeleteDbCommandBuilder : DbCommandBuilder, IDmlDbCommandBuilder
    {
        readonly string _deleteFromFormat = "DELETE FROM {0}";
        readonly string _equalsString = " = ";
        readonly string _whereString = " WHERE";
        readonly string _andString = " AND ";
        readonly string _whereParameterPrefix = "W_";
        readonly string _commandToken = SqlCommandToken.Delete;

        public DbCommandSet CreateCommand(string tableName, SqlResult valueBindingSet, SqlResult whereBindingSet)
        {
            return CreateCommand(tableName, whereBindingSet);
        }

        public DbCommandSet CreateCommand(string tableName, SqlResult bindingSet)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(String.Format(_deleteFromFormat, tableName));
            stringBuilder.Append(MakeWhereParameterString(bindingSet.ColumnNames));

            DbCommandSet commandSet = new DbCommandSet();
            commandSet.DbCommand = GetIDbCommand(stringBuilder.ToString());

            var whereParameters = GetDataParameters(commandSet.DbCommand, bindingSet, _whereParameterPrefix);
            commandSet.DbCommand.Parameters.AddRange(whereParameters);

            commandSet.SqlInfo = MakeSqlInfo(stringBuilder.ToString());
            commandSet.SqlInfo.SqlCommandToken = _commandToken;

            this.CommandList.Enqueue(commandSet);

            return commandSet;
        }

        string MakeWhereParameterString(List<string> columnNames)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(_whereString);

            for (int columnIndex = 0; columnIndex < columnNames.Count; columnIndex++)
            {
                if (columnIndex == 0)
                    stringBuilder.Append(GetWhereColumnNameString(columnNames[columnIndex]));
                else
                    stringBuilder.Append(GetWhereColumnNameString(columnNames[columnIndex], _andString));

                stringBuilder.Append(_equalsString);
                stringBuilder.Append(GetParemeterNameString(columnNames[columnIndex], _whereParameterPrefix));
            }

            return stringBuilder.ToString();
        }

        string GetWhereColumnNameString(string columnName, string delimiter = null)
        {
            if (delimiter == null)
                return Constants.StringSpace + StringUtil.GetDoubleQuotedText(columnName);

            return delimiter + StringUtil.GetDoubleQuotedText(columnName);
        }

        string GetParemeterNameString(string columnName, string prefix)
        {
            return this.DataAccess.GetParameterString(prefix + columnName);
        }

    }
}
