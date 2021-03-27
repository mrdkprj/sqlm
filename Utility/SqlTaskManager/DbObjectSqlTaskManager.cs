using MasudaManager.DataAccess;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    public class DbObjectSqlTaskManager : ISqlTaskManager
    {
        IObserver _observer;
        ISqlTask _currentTask = NullSqlTask.GetInstance();
        ISqlTaskFactory _factory;
        IDbCommandBuilder _builder = new DbCommandBuilder();
        CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public ISqlTaskFactory Factory { get { return _factory; } }
        public Action<ISqlTask> TaskStartAction { get; set; }
        public Action<ISqlTask, TaskCompleteEventArgs> TaskCompleteAction { get; set; }
        public ISqlTask CurrentTask { get { return _currentTask; } }
        public SqlTaskOnErrorActionType DefaultErrorAction { get; set; }

        public DbObjectSqlTaskManager(IObserver observer, ISynchronizeInvoke invoker)
        {
            _observer = observer;
            _factory = new DbObjectSqlTaskFactory(this, invoker);
        }

        public async Task RunSqlTask(object guid, string sql, bool attachObserver)
        {
            await RunSqlTask(guid, PrepareDbCommandBuilder(sql), attachObserver);
        }

        public async Task RunSqlTask(object guid, IDbCommandBuilder dbCommands, bool attachObserver)
        {
            ISqlTask sqlTask = _factory.GetSqlTask(guid);
            await Run(sqlTask, dbCommands);
        }

        IDbCommandBuilder PrepareDbCommandBuilder(string sql)
        {
            _builder.Clear();
            _builder.CreateCommand(sql);
            return _builder;
        }

        public async Task Run(ISqlTask sqlTask, IDbCommandBuilder dbCommands)
        {
            _currentTask = sqlTask;

            if (sqlTask.Cancelable)
                await sqlTask.Run(dbCommands, RenewCancellationTokenSource());
            else
                await sqlTask.Run(dbCommands, CancellationToken.None);
        }

        CancellationToken RenewCancellationTokenSource()
        {
            _tokenSource = new CancellationTokenSource();
            return _tokenSource.Token;
        }

        public bool IsProcessBusy(object guid)
        {
            return _factory.GetSqlTask(guid).IsServiceBusy;
        }

        public bool IsProcessBusy()
        {
            return _factory.GetSqlTasks().Any(s => s.IsServiceBusy);
        }

        public void Cancel()
        {
            if (!_tokenSource.IsCancellationRequested)
                _tokenSource.Cancel();
        }

        public void Release(IObserver observer)
        {
            foreach (var sqlTask in _factory.GetSqlTasks())
            {
                sqlTask.Detach(observer);
            }
        }

        public void FilterTaskResult(object guid, string keyString)
        {
            IDbObjectSqlTask objectSqlTask = _factory.GetSqlTask(guid) as IDbObjectSqlTask;

            if (objectSqlTask != null)
                objectSqlTask.Filter(keyString);
        }

        public void ClearFilter(object guid)
        {
            IDbObjectSqlTask objectSqlTask = _factory.GetSqlTask(guid) as IDbObjectSqlTask;

            if (objectSqlTask != null)
                objectSqlTask.ClearFilter();
        }

        public void Update(object sender)
        {
            _observer.Update(sender);
        }

        public void Complete(object sender)
        {
            _observer.Complete(sender);
        }

        public void Error(object sender, Exception e)
        {
            _observer.Error(sender, e);
        }
    }
}
