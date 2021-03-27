
namespace MasudaManager.Utility
{
    public struct SqlMessageFormat
    {
        private readonly string _format;
        private readonly int _parameterCount;
        public string Format { get { return _format; } }
        public int ParameterCount { get { return _parameterCount; } }

        public SqlMessageFormat(string format, int parameterCount)
        {
            _format = format;
            _parameterCount = parameterCount;
        }
    }
}
