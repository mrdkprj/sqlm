using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager
{
    public class SqlTaskRequest
    {
        public SqlTaskRequest()
        {
            Initialize();
        }

        public SqlTaskRequest(object guid, SqlType sqlType, bool notifyOnUpdate)
        {
            this.Guid = guid;
            this.ParsedSqlType = sqlType;
            this.NotifyOnUpdate = notifyOnUpdate;
        }

        public object Guid { get; set; }
        public SqlType ParsedSqlType { get; set; }
        public bool NotifyOnUpdate { get; set; }

        public void Initialize()
        {
            this.Guid = null;
            this.ParsedSqlType = SqlType.Empty;
            this.NotifyOnUpdate = false;
        }

        public void RenewRequest(object guid, SqlType sqlType, bool notifyOnUpdate)
        {
            this.Guid = guid;
            this.ParsedSqlType = sqlType;
            this.NotifyOnUpdate = notifyOnUpdate;
        }
    }
}
