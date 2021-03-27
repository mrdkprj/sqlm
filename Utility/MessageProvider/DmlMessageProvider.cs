using System;

namespace MasudaManager.Utility
{
    sealed class DmlMessageProvider : IMessageProvider
    {
        string _message = null;

        static readonly DmlMessageProvider _instance = new DmlMessageProvider();
        static DmlMessageProvider() { }
        private DmlMessageProvider() { }
        public static DmlMessageProvider Instance
        {
            get { return _instance; }
        }
        
        public string GetMessage()
        {
            return _message;
        }

        public string GetMessage(SqlBaseInfo sqlBaseInfo)
        {
            _message = String.Format(GetMessageFormat(sqlBaseInfo.SqlCommandToken).Format, sqlBaseInfo.RecordCount);

            return _message;
        }

        SqlMessageFormat GetMessageFormat(string token)
        {
            switch (token)
            {
                case SqlCommandToken.Delete:
                    return new SqlMessageFormat("{0} rows deleted.", 1);
                case SqlCommandToken.Insert:
                    return new SqlMessageFormat("{0} rows inserted.", 1);
                case SqlCommandToken.LockTable:
                    return new SqlMessageFormat("Table locked.", 1);
                case SqlCommandToken.Update:
                    return new SqlMessageFormat("{0} rows updated.", 1);
                case SqlCommandToken.Call:
                case SqlCommandToken.ExplainPlan:
                case SqlCommandToken.Merge:
                    return new SqlMessageFormat("SQL complete.", 0);
            }

            throw new ArgumentException("Cannot get SQL message.");
        }
    }
}
