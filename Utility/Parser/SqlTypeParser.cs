using System;
using System.Linq;

namespace MasudaManager.Utility
{
    public static class SqlTypeParser
    {
        public static SqlType Parse(string ruleName)
        {
            if (SqlCommandToken.QueryCommandTokens.Any(command => ruleName.Contains(command, StringComparison.OrdinalIgnoreCase)))
                return SqlType.Query;

            if (SqlCommandToken.DmlCommandTokens.Any(command => ruleName.Contains(command, StringComparison.OrdinalIgnoreCase)))
                return SqlType.DML;

            if (SqlCommandToken.DdlCommandTokens.Any(command => ruleName.Contains(command, StringComparison.OrdinalIgnoreCase)))
                return SqlType.DDL;

            if (SqlCommandToken.TclCommandTokens.Any(command => ruleName.Contains(command, StringComparison.OrdinalIgnoreCase)))
                return SqlType.TCL;

            return SqlType.Invalid;
        }

        public static string GetSqlCommandToken(string ruleName)
        {
            return SqlCommandToken.Tokens.FirstOrDefault(token => ruleName.Contains(token, StringComparison.OrdinalIgnoreCase));
        }

        public static SqlCommandType GetSqlCommandType(SqlType sqlType)
        {
            switch (sqlType)
            {
                case SqlType.Query:
                    return SqlCommandType.Query;
                case SqlType.DML:
                case SqlType.DML | SqlType.DDL:
                case SqlType.DML | SqlType.TCL:
                case SqlType.DDL:
                case SqlType.DDL | SqlType.TCL:
                case SqlType.TCL:
                case SqlType.DML | SqlType.DDL | SqlType.TCL:
                    return SqlCommandType.NonQuery;
                case SqlType.Invalid:
                    return SqlCommandType.Invalid;
                default:
                    return SqlCommandType.Empty;
            }

        }
    }


}
