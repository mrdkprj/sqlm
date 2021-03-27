using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasudaManager.Utility
{
    sealed class DdlMessageProvider : IMessageProvider
    {
        string _message = null;

        static readonly DdlMessageProvider _instance = new DdlMessageProvider();
        static DdlMessageProvider() { }
        private DdlMessageProvider() { }
        public static DdlMessageProvider Instance
        {
            get { return _instance; }
        }

        public string GetMessage()
        {
            return _message;
        }

        public string GetMessage(SqlBaseInfo sqlBaseInfo)
        {
            SqlMessageFormat format = GetMessageFormat(sqlBaseInfo.SqlCommandToken, sqlBaseInfo.ExecutedSql);

            List<string> sqlValues = sqlBaseInfo.ExecutedSql.Split(new char[] { Constants.CharSpace }, StringSplitOptions.RemoveEmptyEntries).ToList();

            string formattedString = String.Format(format.Format, sqlValues.GetRange(0, format.ParameterCount).ToArray());

            _message = char.ToUpper(formattedString[0]) + formattedString.Substring(1).ToLower();

            return _message;
        }

        SqlMessageFormat GetMessageFormat(string token, string sql)
        {
            switch (token)
            {
                case SqlCommandToken.Alter:
                    return new SqlMessageFormat("{1} altered.", 2);
                case SqlCommandToken.Comment:
                    return new SqlMessageFormat("Comment created on {2}.", 3);
                case SqlCommandToken.Create:
                    return GetCreateStmtMessageFormat(sql);
                case SqlCommandToken.Drop:
                    return new SqlMessageFormat("{1} dropped.", 2);
                case SqlCommandToken.Purge:
                    return new SqlMessageFormat("{1} {0} purged.", 2);
                case SqlCommandToken.Truncate:
                    return new SqlMessageFormat("{1} truncated.", 2);
                case SqlCommandToken.Analyze:
                case SqlCommandToken.AssociateStatistics:
                case SqlCommandToken.Audit:
                case SqlCommandToken.DisassociateStatistics:
                case SqlCommandToken.Flashback:
                case SqlCommandToken.Grant:
                case SqlCommandToken.Noaudit:
                case SqlCommandToken.Rename:
                case SqlCommandToken.Revoke:
                    return new SqlMessageFormat("SQL complete.", 0);
            }

            throw new ArgumentException("Cannot get SQL message.");
        }

        SqlMessageFormat GetCreateStmtMessageFormat(string sql)
        {
            if (sql.Contains("unique", StringComparison.OrdinalIgnoreCase))
                return new SqlMessageFormat("{1} {2} created.", 3);

            return new SqlMessageFormat("{1} created.", 2);
        }
    }
}
