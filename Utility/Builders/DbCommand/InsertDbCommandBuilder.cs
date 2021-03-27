using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MasudaManager.DataAccess;

namespace MasudaManager.Utility
{
    public class InsertDbCommandBuilder : DbCommandBuilder, IDmlDbCommandBuilder
    {
        readonly string _insertIntoFormat = "INSERT INTO {0} VALUES(";
        readonly string _valueParameterPrefix = "V_";
        readonly string _commandToken = SqlCommandToken.Insert;

        public DbCommandSet CreateCommand(string tableName, SqlResult valueBindingSet, SqlResult whereBindingSet)
        {
            return CreateCommand(tableName, valueBindingSet);
        }

        public DbCommandSet CreateCommand(string tableName, SqlResult bindingSet)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(String.Format(_insertIntoFormat, tableName));
            stringBuilder.Append(MakeValuesParameterString(bindingSet.ColumnNames));
            stringBuilder.Append(Constants.StringRightParenthesis);

            DbCommandSet commandSet = new DbCommandSet();
            commandSet.DbCommand = GetIDbCommand(stringBuilder.ToString());

            var valueParameters = GetDataParameters(commandSet.DbCommand, bindingSet, _valueParameterPrefix);
            commandSet.DbCommand.Parameters.AddRange(valueParameters);

            commandSet.SqlInfo = MakeSqlInfo(stringBuilder.ToString());
            commandSet.SqlInfo.SqlCommandToken = _commandToken;

            this.CommandList.Enqueue(commandSet);

            return commandSet;
        }

        string MakeValuesParameterString(IEnumerable<string> columnNames)
        {
            return String.Join(Constants.StringComma, GetColumnNameParameterList(columnNames, _valueParameterPrefix));
        }

        IEnumerable<string> GetColumnNameParameterList(IEnumerable<string> columnNames, string prefix)
        {
            foreach (var columnName in columnNames)
            {
                yield return GetParemeterNameString(columnName, columnName);
            }
        }

        string GetParemeterNameString(string columnName, string prefix)
        {
            return this.DataAccess.GetParameterString(prefix + columnName);
        }
    }
}
