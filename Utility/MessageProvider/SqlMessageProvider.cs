using System;

namespace MasudaManager.Utility
{
    public sealed class SqlMessageProvider : IMessageProvider
    {
        string _message = null;

        public string GetMessage()
        {
            return _message;
        }

        public string GetMessage(SqlBaseInfo sqlBaseInfo)
        {
            IMessageProvider provider = GetMessageProvider(sqlBaseInfo);
            _message = provider.GetMessage(sqlBaseInfo);

            return _message;
        }

        IMessageProvider GetMessageProvider(SqlBaseInfo sqlBaseInfo)
        {
            switch (sqlBaseInfo.SqlType)
            {
                case SqlType.Query:
                    return QueryMessageProvider.Instance;
                case SqlType.DML:
                    return DmlMessageProvider.Instance;
                case SqlType.DDL:
                    return DdlMessageProvider.Instance;
                case SqlType.TCL:
                    return TclMessageProvider.Instance;
                default:
                    return NullMessageProvider.Instance;
            }

        }

    }
}
