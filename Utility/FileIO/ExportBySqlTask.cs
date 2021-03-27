using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace MasudaManager.Utility
{
    public abstract class ExportBySqlTask : IExportStrategy, IObserver
    {
        readonly FileMode _fileMode = FileMode.Create;
        readonly FileAccess _fileAccess = FileAccess.Write;
        
        ExportModel _exportModel;
        List<SqlResult> _exportData = new List<SqlResult>();
        List<IObserver> _observers = new List<IObserver>();
        ISqlTaskManager _sqlTaskManager;
        Task _task;
        CancellationTokenSource _tokenSource;
        int _exportCount = 0;
        int _exportProgress = 0;

        #region Property

        public bool IsBusy { get { return !_task.IsCompleted; } }
        public int ExportCount { get { return _exportCount; } }
        public int ExportProgress { get { return _exportProgress; } }
        protected ExportModel ExportModel { get { return _exportModel; } }
        protected List<SqlResult> ExportData { get { return _exportData; } }
        protected FileMode FileMode { get { return _fileMode; } }
        protected FileAccess FileAccess { get { return _fileAccess; } }

        #endregion

        #region Constructor
        public ExportBySqlTask(ExportModel exportModel)
        {
            _exportModel = exportModel;
            _sqlTaskManager = new SqlTaskManager(this, null);
            _sqlTaskManager.DefaultErrorAction = SqlTaskOnErrorActionType.Cancel;
        }
        #endregion

        #region Export
        public async Task Export()
        {
            _exportCount = 0;
            _exportProgress = 0;
            _tokenSource = new CancellationTokenSource();

            try
            {
                _task = Task.Run(async () =>
                    {
                        await _sqlTaskManager.RunSqlTask(this, _exportModel.ExportSql, true);
                        WriteRetrievedSqlResults(_tokenSource.Token);
                    });

                await _task;

                NotifyComplete();
            }
            catch (AggregateException aggre)
            {
                NotifyError(aggre.InnerException);
            }
            catch (Exception ex)
            {
                NotifyError(ex);
            }
        }
        #endregion

        #region Abstract method
        abstract protected void WriteRetrievedSqlResults(CancellationToken token);
        #endregion

        #region Update progress
        protected void UpdateProgress()
        {
            _exportCount++;
            _exportProgress = (_exportCount * 100) / _exportData.Count;
        }
        #endregion

        #region Cancel
        public void Cancel()
        {
            _sqlTaskManager.Cancel();

            if (!_tokenSource.IsCancellationRequested)
                _tokenSource.Cancel();            
        }
        #endregion

        #region Release
        public void Release(IObserver observer)
        {
            _sqlTaskManager.Release(observer);
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

        #region Observer
        public void Update(object sender)
        {

        }

        public void Complete(object sender)
        {
            using (var sqlTask = _sqlTaskManager.CurrentTask)
            {
                _exportData = sqlTask.BindingModel.ToSourceList();
            }
        }

        public void Error(object sender, Exception e)
        {
            throw e;
        }
        #endregion
    }
}
