using System;

namespace MasudaManager.Utility
{
    sealed class TclMessageProvider : IMessageProvider
    {
        string _message = null;

        static readonly TclMessageProvider _instance = new TclMessageProvider();
        static TclMessageProvider() { }
        private TclMessageProvider() { }
        public static TclMessageProvider Instance
        {
            get { return _instance; }
        }

        public string GetMessage()
        {
            return _message;
        }

        public string GetMessage(SqlBaseInfo sqlBaseInfo)
        {
            _message = CreateMessage(sqlBaseInfo.SqlCommandToken);

            return _message;
        }

        string CreateMessage(string token)
        {
            switch (token)
            {
                case SqlCommandToken.Commit:
                    return "Commit complete.";
                case SqlCommandToken.Rollback:
                    return "Rollback complate.";
                case SqlCommandToken.Savepoint:
                    return "Savepoint created.";
            }

            throw new ArgumentException("Cannot get SQL message.");
        }
    }
}
