using MasudaManager.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    public abstract class ImportBySqlTask : IImportStrategy, IObserver
    {
        readonly FileMode _fileMode = FileMode.Open;
        readonly FileAccess _fileAccess = FileAccess.Read;
        //readonly int _maxCount = 100;

        ImportModel _importModel;
        ISqlTaskManager _sqlTaskManager;
        List<IObserver> _observers = new List<IObserver>();
        Task _task;
        CancellationTokenSource _tokenSource;
        int _totalCount = 0;
        int _importCount = 0;
        int _importProgress = 0;

        #region Property
        public bool IsBusy { get { return !_task.IsCompleted; } }
        public int ImportCount { get { return _importCount; } }
        public int ImportProgress { get { return _importProgress; } }
        protected ImportModel ImportModel { get { return _importModel; } }
        protected FileMode FileMode { get { return _fileMode; } }
        protected FileAccess FileAccess { get { return _fileAccess; } }
        #endregion

        #region Constructor
        public ImportBySqlTask(ImportModel importModel)
        {
            _importModel = importModel;
            _sqlTaskManager = new SqlTaskManager(this, null);
            _sqlTaskManager.DefaultErrorAction = SqlTaskOnErrorActionType.Cancel;
            _sqlTaskManager.TaskCompleteAction = OnTaskComplete;
        }
        #endregion

        #region Import
        public async Task Import()
        {
            _importCount = 0;
            _totalCount = 0;
            _tokenSource = new CancellationTokenSource();

            try
            {
                //_task = Task.Factory.StartNew(() => GetInsertCommands(_tokenSource.Token))
                //            .ContinueWith
                //            (
                //                antecedent => _sqlTaskManager.RunSqlTask(this, antecedent.Result, true)
                //            )
                //            .Unwrap();
                _task = Task.Factory.StartNew(() => GetInsertCommands(_tokenSource.Token))
                            .ContinueWith
                            (
                                antecedent => RunSqlTaskOnTaskComplete(antecedent)
                            )
                            .Unwrap();

                await _task;

                NotifyComplete();
            }
            catch (AggregateException aggre)
            {
                NotifyError(aggre.InnerException);
            }
            catch (Exception e)
            {
                NotifyError(e);
            }
        }
        #endregion

        #region Abstract method
        abstract protected IDbCommandBuilder GetInsertCommands(CancellationToken token);
        #endregion

        #region RunSqlTaskOnTaskComplete
        async Task<IDbCommandBuilder> RunSqlTaskOnTaskComplete(Task<IDbCommandBuilder> task)
        {
            if (task.Exception == null)
                await _sqlTaskManager.RunSqlTask(this, task.Result, true);

            return task.Result;
        }
        #endregion

        #region SetTotalCount
        protected void SetTotalCount(int totalCount)
        {
            _totalCount = totalCount;
        }
        #endregion

        #region Update progress
        protected void UpdateProgress()
        {
            _importCount++;
            _importProgress = (_importCount * 100) / _totalCount;
        }
        #endregion

        #region Commit/Rollback
        void Commit()
        {
            _sqlTaskManager.CurrentTask.Commit();
        }

        void Rollback()
        {
            _sqlTaskManager.CurrentTask.Rollback();
        }
        #endregion

        #region Cancel
        public void Cancel()
        {
            if (!_tokenSource.IsCancellationRequested)
                _tokenSource.Cancel();

            _sqlTaskManager.Cancel();
        }
        #endregion
        
        #region Release observer
        public void Release(IObserver observer)
        {
            if (!_tokenSource.IsCancellationRequested)
                _tokenSource.Cancel(); 
            
            _sqlTaskManager.Release(observer);
        }
        #endregion

        #region OnTaskComplete
        void OnTaskComplete(ISqlTask sqlTask, TaskCompleteEventArgs e)
        {
            if (e.Status == SqlTaskStatus.Complete)
                Commit();                
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
            UpdateProgress();
            Notify();
        }

        public void Complete(object sender)
        {
        }

        public void Error(object sender, Exception e)
        {
            Rollback();
            throw e;
        }
        #endregion
    }
}
