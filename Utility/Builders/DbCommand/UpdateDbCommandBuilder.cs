using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MasudaManager.DataAccess;
using MasudaManager.Utility.Preference;

namespace MasudaManager.Utility
{
    public class UpdatePreparedDbCommandBuilder : DbCommandBuilder, IDmlDbCommandBuilder
    {
        readonly string _updateFormat = "UPDATE {0}";
        readonly string _delimiterStringAnd = " AND ";
        readonly string _delimiterStringComma = Constants.StringComma;
        readonly string _equalsString = " = ";
        readonly string _setString = " SET";
        readonly string _whereString = " WHERE";
        readonly string _whereParameterPrefix = "W_";
        readonly string _setParameterPrefix = "S_";
        readonly string _commandToken = SqlCommandToken.Update;

        public DbCommandSet CreateCommand(string tableName, SqlResult bindingSet)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(String.Format(_updateFormat, tableName));
            stringBuilder.Append(MakeSetParameterString(bindingSet.ColumnNames));

            DbCommandSet commandSet = new DbCommandSet();
            commandSet.DbCommand = GetIDbCommand(stringBuilder.ToString());

            var setParameters = GetDataParameters(commandSet.DbCommand, bindingSet, _setParameterPrefix);
            commandSet.DbCommand.Parameters.AddRange(setParameters);

            commandSet.SqlInfo = MakeSqlInfo(stringBuilder.ToString());
            commandSet.SqlInfo.SqlCommandToken = _commandToken;

            this.CommandList.Enqueue(commandSet);

            return commandSet;
        }

        
        public DbCommandSet CreateCommand(string tableName, SqlResult valueBindingSet, SqlResult whereBindingSet)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(String.Format(_updateFormat, tableName));
            stringBuilder.Append(MakeSetParameterString(valueBindingSet.ColumnNames));
            stringBuilder.Append(MakeWhereParameterString(whereBindingSet.ColumnNames));

            DbCommandSet commandSet = new DbCommandSet();
            commandSet.DbCommand = GetIDbCommand(stringBuilder.ToString());

            var setParameters = GetDataParameters(commandSet.DbCommand, valueBindingSet, _setParameterPrefix);
            commandSet.DbCommand.Parameters.AddRange(setParameters);

            var whereParameters = GetDataParameters(commandSet.DbCommand, whereBindingSet, _whereParameterPrefix);
            commandSet.DbCommand.Parameters.AddRange(whereParameters);
            
            commandSet.SqlInfo = MakeSqlInfo(stringBuilder.ToString());
            commandSet.SqlInfo.SqlCommandToken = _commandToken;

            this.CommandList.Enqueue(commandSet);

            return commandSet;
        }

        string MakeSetParameterString(List<string> columnNames)
        {
            return MakeParameterString(columnNames, _setString, Constants.StringComma, _setParameterPrefix);
        }

        string MakeWhereParameterString(List<string> columnNames)
        {
            return MakeParameterString(columnNames, _whereString, _delimiterStringAnd, _whereParameterPrefix);
        }

        string MakeParameterString(List<string> columnNames, string initialString, string delimiterString, string prefix)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(initialString);
            for (int cnt = 0; cnt < columnNames.Count; cnt++)
            {
                if (cnt == 0)
                    stringBuilder.Append(GetColumnNameString(columnNames[cnt]));
                else
                    stringBuilder.Append(GetColumnNameString(columnNames[cnt], delimiterString));

                stringBuilder.Append(_equalsString);
                stringBuilder.Append(GetParemeterNameString(columnNames[cnt], prefix));
            }

            return stringBuilder.ToString();
        }

        string GetColumnNameString(string columnName, string delimiter = null)
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
