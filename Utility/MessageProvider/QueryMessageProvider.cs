using System;

namespace MasudaManager.Utility
{
    sealed class QueryMessageProvider : IMessageProvider
    {
        string _message = null;

        static readonly QueryMessageProvider _instance = new QueryMessageProvider();
        static QueryMessageProvider() { }
        private QueryMessageProvider() { }
        public static QueryMessageProvider Instance
        {
            get { return _instance; }
        }

        public string GetMessage()
        {
            return _message;
        }

        public string GetMessage(SqlBaseInfo sqlBaseInfo)
        {
            _message = String.Format(GetMessageFormat().Format, sqlBaseInfo.RecordCount);

            return _message;
        }

        SqlMessageFormat GetMessageFormat()
        {
            return new SqlMessageFormat("{0} rows selected.", 1);
        }
    }
}
