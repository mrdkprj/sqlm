using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    public abstract class SqlTask : ISqlTask
    {
        public event EventHandler TaskStart;
        public event EventHandler<TaskCompleteEventArgs> TaskComplete;

        ISynchronizeInvoke _invoker;
        ISqlService _sqlService;
        SqlBaseInfo _sqlBaseInfo = new SqlBaseInfo();
        QueryResultBindingList _bindingList;
        SqlMessageProvider _messageProvider = new SqlMessageProvider();
        List<IObserver> _observers = new List<IObserver>();
        bool _disposed = false;
        SafeHandle _handle = new SafeFileHandle(IntPtr.Zero, true);
        SqlTaskStatus _status;

        #region Property
        public object Guid { get; set; }
        public bool IsServiceBusy { get { return _sqlService.IsBusy; } }
        public bool Cancelable { get; set; }
        public ISqlService SqlService
        {
            get { return _sqlService; }
            set { _sqlService = value; }
        }
        public SqlBaseInfo ExcutedSqlInfo { get { return _sqlBaseInfo; } }
        public IEnumerable<SqlResult> SqlResults { get { return _bindingList.ToSourceList(); } }
        public DynamicSortableBindingList<SqlResult, QueryResult> BindingModel { get { return _bindingList; } }
        public SqlTaskStatus Status { get { return _status; } }

        protected ISynchronizeInvoke Invoker { get { return _invoker; } }
        protected ISqlService Service { get { return _sqlService; } }
        protected SqlBaseInfo SqlBaseInfo
        {
            get { return _sqlBaseInfo; }
            set { _sqlBaseInfo = value; }
        }
        protected QueryResultBindingList BindingList
        {
            get { return _bindingList; }
            set { _bindingList = value; }
        }
        protected SqlMessageProvider MessageProvider { get { return _messageProvider; } }
        #endregion

        #region Constructor
        public SqlTask(ISynchronizeInvoke invoker, ISqlService service)
        {
            _invoker = invoker;
            _sqlService = service;
            _bindingList = new QueryResultBindingList(invoker);
        }
        #endregion

        #region [Template] Run
        protected abstract void InitializeSqlService();
        protected abstract Task ExecuteSqlService(IDbCommandBuilder dbCommands, CancellationToken token);
        protected abstract void FinalizeSqlService();

        public async Task Run(IDbCommandBuilder dbCommands, CancellationToken token)
        {
            InitializeSqlService();
 
            _status = SqlTaskStatus.Initiated;

            await ExecuteSqlService(dbCommands, token);

            FinalizeSqlService();
        }
        #endregion

        #region Commit/Rollback
        public bool Commit()
        {
            return _sqlService.Commit();
        }

        public bool Rollback()
        {
            return _sqlService.Rollback();
        }
        #endregion

        #region [Abstract] OnSqlServiceUpdate
        protected abstract void OnSqlServiceUpdate();
        protected abstract void OnSqlServiceComplete();
        protected abstract void OnSqlServiceError(Exception e);
        #endregion

        #region OnTaskStart
        protected virtual void OnTaskStart(object sender, EventArgs e)
        {
            if (this.TaskStart != null)
                this.TaskStart(sender, e);
        }
        #endregion

        #region OnTaskComplete
        protected virtual void OnTaskComplete(object sender, TaskCompleteEventArgs e)
        {
            if (this.TaskComplete != null)
                this.TaskComplete(sender, e);
        }
        #endregion

        #region Clear
        public void Clear()
        {
            _bindingList.Clear();
        }
        #endregion

        #region Observer
        public void Update(object sender)
        {
            _status = SqlTaskStatus.Running;
            OnSqlServiceUpdate();
        }

        public void Complete(object sender)
        {
            _status = SqlTaskStatus.Complete;
            OnSqlServiceComplete();
        }

        public void Error(object sender, Exception e)
        {
            if (e.GetType().Equals(typeof(OperationCanceledException)))
                _status = SqlTaskStatus.Cancelled;
            else
                _status = SqlTaskStatus.Error;

            OnSqlServiceError(e);
        }
        #endregion

        #region Observable
        public void Attach(IObserver observer)
        {
            lock (_observers)
            {
                _observers.Add(observer);
            }
        }

        public void Detach(IObserver observer)
        {
            lock (_observers)
            {
                _observers.Remove(observer);
            }
        }

        public void Notify()
        {
            lock (_observers)
            {
                foreach (var observer in _observers)
                {
                    observer.Update(this);
                }
            }
        }

        public void NotifyComplete()
        {
            lock (_observers)
            {
                foreach (var observer in _observers)
                {
                    observer.Complete(this);
                }
            }
        }

        public void NotifyError(Exception e)
        {
            lock (_observers)
            {
                foreach (var observer in _observers)
                {
                    observer.Error(this, e);
                }
            }
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _handle.Dispose();
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            _disposed = true;
        }
        #endregion

    }
}
