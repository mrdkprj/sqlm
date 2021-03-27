using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    public class NullSqlTask : ISqlTask, IDbObjectSqlTask
    {
        public event EventHandler TaskStart;
        public event EventHandler<TaskCompleteEventArgs> TaskComplete;

        SqlBaseInfo _sqlBaseInfo = new SqlBaseInfo();
        static NullSqlTask _instance = new NullSqlTask();
        static NullSqlTask() { }
        private NullSqlTask() { }        
        public static NullSqlTask GetInstance()
        {
            return _instance;
        }

        public DynamicSortableBindingList<SqlResult, QueryResult> BindingModel { get { return NullQueryResultBindingList.Instance; } }
        public bool Cancelable { get; set; }
        public object Guid { get; set; }
        public SqlBaseInfo ExcutedSqlInfo { get { return _sqlBaseInfo; } }
        public bool IsServiceBusy { get { return false; } }
        public bool NotifyOnTaskComplete { get; set; }
        public DbObjectPropertyType PropertyType { get; set; }
        public IEnumerable<SqlResult> SqlResults { get { return null; } }
        public ISqlService SqlService
        {
            get { return NullSqlService.GetInstance(); }
            set { ; }
        }
        public IDbObjectStrategy Strategy { get; set; }
        public SqlTaskStatus Status { get { return SqlTaskStatus.None; } }

        public async Task Run(IDbCommandBuilder commands, CancellationToken token)
        {
        }

        public bool Commit() { return true; }

        public bool Rollback() { return true; }

        public void Filter(string keystring) { }

        public void ClearFilter() { }

        public void Clear() { }

        public void Dispose() { }

        public void Update(object sender) { }

        public void Complete(object sender) { }

        public void Error(object sender, Exception e) { }

        public void Attach(IObserver observer) { }

        public void Detach(IObserver observer) { }

        public void Notify() { }

        public void NotifyComplete() { }

        public void NotifyError(Exception e) { }
    }
}
