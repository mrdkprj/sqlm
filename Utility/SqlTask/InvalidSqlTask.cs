using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MasudaManager.Utility
{
    public class InvalidSqlTask : ISqlTask
    {
        public event EventHandler TaskStart
        {
            add
            {
                if (_taskStart == null)
                    _taskStart += value;
            }
            remove
            {
                _taskStart -= value;
            }
        }

        public event EventHandler<TaskCompleteEventArgs> TaskComplete
        {
            add
            {
                if (_taskComplete == null)
                    _taskComplete += value;
            }
            remove
            {
                _taskComplete -= value;
            }
        }
        
        EventHandler _taskStart;
        EventHandler<TaskCompleteEventArgs> _taskComplete;
        IObserver _observer;
        SqlBaseInfo _sqlBaseInfo = new SqlBaseInfo();

        static InvalidSqlTask _instance = new InvalidSqlTask();
        static InvalidSqlTask() { }
        private InvalidSqlTask() { }        
        public static InvalidSqlTask GetInstance()
        {
            return _instance;
        }

        public DynamicSortableBindingList<SqlResult, QueryResult> BindingModel { get { return NullQueryResultBindingList.Instance; } }
        public bool Cancelable { get; set; }
        public object Guid { get; set; }
        public SqlBaseInfo ExcutedSqlInfo { get { return _sqlBaseInfo; } }
        public bool IsServiceBusy { get { return false; } }
        public bool NotifyOnTaskComplete { get; set; }
        public IEnumerable<SqlResult> SqlResults { get { return null; } }
        public ISqlService SqlService
        {
            get { return NullSqlService.GetInstance(); }
            set { ; }
        }
        public SqlTaskStatus Status { get { return SqlTaskStatus.Error; } }

        public async Task Run(IDbCommandBuilder commands, System.Threading.CancellationToken token)
        {
            OnTaskStart(this, EventArgs.Empty);

            ConfigureSqlBaseInfo(commands);

            NotifyError(_sqlBaseInfo.SqlException);

            OnTaskComplete(this, new TaskCompleteEventArgs(SqlTaskResultType.Nothing, this.Status));
        }

        void OnTaskStart(object sender, EventArgs e)
        {
            if (_taskStart != null)
                _taskStart(this, EventArgs.Empty);
        }

        void OnTaskComplete(object sender, TaskCompleteEventArgs e)
        {
            if (_taskComplete != null)
                _taskComplete(this, e);
        }

        void ConfigureSqlBaseInfo(IDbCommandBuilder commands)
        {
            _sqlBaseInfo = commands.FirstOrDefault().SqlInfo;
            if (_sqlBaseInfo.SqlException != null)
            {
                _sqlBaseInfo.HasError = true;
                _sqlBaseInfo.SqlMessage = _sqlBaseInfo.SqlException.Message;
            }
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

        public void Attach(IObserver observer)
        {
            _observer = observer;
        }

        public void Detach(IObserver observer) 
        {
            _observer = null;
        }

        public void Notify() { }

        public void NotifyComplete() { }

        public void NotifyError(Exception e)
        {
            if (_observer != null)
                _observer.Error(this, e);
        }
    }
}
