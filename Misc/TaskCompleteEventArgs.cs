using System;
using System.Threading.Tasks;

namespace MasudaManager
{
    public class TaskCompleteEventArgs : EventArgs
    {
        public TaskCompleteEventArgs(SqlTaskResultType result, SqlTaskStatus status)
        {           
            this.Result = result;
            this.Status = status;
        }

        public SqlTaskResultType Result { get; private set; }
        public SqlTaskStatus Status { get; private set; }
    }
}
