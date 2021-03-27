using System;

namespace MasudaManager
{
    public class SqlBaseInfo
    {
        public SqlBaseInfo()
        {
            Initialize();
        }

        public string ExecutedSql { get; set; }
        public bool HasError { get; set; }
        public bool HasRows { get; set; }
        public string InputSql { get; set; }
        public int RecordCount { get; set; }
        public string SqlCommandToken { get; set; }
        public Exception SqlException { get; set; }
        public string SqlMessage { get; set; }
        public SqlType SqlType { get; set; }

        public void Initialize()
        {
            this.InputSql = null;
            this.ExecutedSql = null;
            this.SqlMessage = null;
            this.SqlType = SqlType.Empty;
            this.HasError = false;
            this.SqlException = new Exception();
            this.HasRows = false;
            this.SqlCommandToken = null;
            this.RecordCount = 0;
        }
    }
}
