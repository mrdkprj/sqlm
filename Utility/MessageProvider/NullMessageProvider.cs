
namespace MasudaManager.Utility
{
    sealed class NullMessageProvider : IMessageProvider
    {
        static readonly NullMessageProvider _instance = new NullMessageProvider();
        static NullMessageProvider() { }
        private NullMessageProvider() { }
        public static NullMessageProvider Instance
        {
            get { return _instance; }
        }

        public string GetMessage()
        {
            return null;
        }

        public string GetMessage(SqlBaseInfo sqlBaseInfo)
        {
            return null;
        }
    }
}
