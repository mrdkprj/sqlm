using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using MasudaManager.DataAccess;
using MasudaManager.Utility.Preference;

namespace MasudaManager.Utility
{
    public class SqlStatementBuilder
    {
        readonly string _selectString = "SELECT";
        readonly string _fromString = "FROM";
        readonly string _insertIntoString = "INSERT INTO";
        readonly string _insertValuesString = "VALUES";
        readonly string _deleteString = "DELETE";
        
        StringBuilder _stringBuilder = new StringBuilder();
        SchemaTableSqlService _sqlService = new SchemaTableSqlService();

        public bool EndsWithSemicolun { get; set; }

        public SqlStatementBuilder() { }

        public SqlStatementBuilder(bool endsWithSemicolon)
        {
            this.EndsWithSemicolun = endsWithSemicolon;
        }

        public string CreateSelectStatement(string tableName)
        {
            return CreateSelectStatement(tableName, GetColumnNameSqlResult(tableName));
        }

        public string CreateSelectStatement(string tableName, SqlResult sqlResult)
        {
            return CreateSelectStatement(tableName, sqlResult.ColumnNames);
        }

        public string CreateSelectStatement(string tableName, IEnumerable<SqlResult> sqlResults)
        {
            return CreateSelectStatement(tableName, sqlResults.SelectMany(s => s.ColumnNames));
        }
        
        public string CreateSelectStatement(string tableName, IEnumerable<string> columnNames)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(_selectString);
            _stringBuilder.Append(Constants.StringSpace);
            int columnCount = columnNames.Count();

            for (int i = 0; i < columnCount; i++)
            {
                if (i == columnCount - 1)
                    _stringBuilder.Append(FormatColumnName(columnNames.ElementAt(i)) + Constants.StringSpace);
                else
                    _stringBuilder.Append(FormatColumnName(columnNames.ElementAt(i)) + Constants.StringComma + Constants.StringSpace);
            }
            _stringBuilder.Append(_fromString);
            _stringBuilder.Append(Constants.StringSpace);
            _stringBuilder.Append(FormatObjectName(tableName));

            return GetSqlStatement();
        }

        public string CreateInsertStatement(string tableName)
        {
            var columnNameSqlResult = GetColumnNameSqlResult(tableName);
            columnNameSqlResult.RowValues = columnNameSqlResult.RowValues.Select(s => s = string.Empty).ToList();
            return CreateInsertStatement(tableName, columnNameSqlResult);
        }

        public string CreateInsertStatement(string tableName, SqlResult sqlResult)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(_insertIntoString);
            _stringBuilder.Append(Constants.StringSpace);
            _stringBuilder.Append(FormatObjectName(tableName));
            _stringBuilder.Append(Constants.StringSpace);
            _stringBuilder.Append(_insertValuesString);
            _stringBuilder.Append(Constants.StringLeftParenthesis);
            for (int i = 0; i < sqlResult.ColumnNames.Count; i++)
            {
                if (i == sqlResult.ColumnNames.Count - 1)
                    _stringBuilder.Append(String.Format(GetInsertValueFormat(sqlResult.ColumnTypes[i]), sqlResult.RowValues[i]) + Constants.StringRightParenthesis);
                else
                    _stringBuilder.Append(String.Format(GetInsertValueFormat(sqlResult.ColumnTypes[i]), sqlResult.RowValues[i]) + Constants.StringComma);
            }

            return GetSqlStatement();
        }

        public string CreateDeleteStatement(string tableName)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(_deleteString);
            _stringBuilder.Append(Constants.StringSpace);
            _stringBuilder.Append(_fromString);
            _stringBuilder.Append(Constants.StringSpace);
            _stringBuilder.Append(FormatObjectName(tableName));

            return GetSqlStatement();
        }

        string GetSqlStatement()
        {
            if (this.EndsWithSemicolun)
                _stringBuilder.Append(Constants.StringSemicolon);

            return _stringBuilder.ToString();
        }

        SqlResult GetColumnNameSqlResult(string tableName)
        {
            return _sqlService.GetColumnInfoFromTable(tableName).FirstOrDefault();
        }

        string FormatColumnName(string columnName)
        {
            return UserPreference.Reflector.GetEnclosedPropertyValue(columnName);
        }

        string FormatObjectName(string objectName)
        {
            return UserPreference.Reflector.GetEnclosedObjectName(objectName);
        }

        string GetInsertValueFormat(Type columnType)
        {
            if (columnType.IsNumericType())
                return "{0}";

            return Constants.CharQuotation + "{0}" + Constants.CharQuotation;
        }
    }
}
