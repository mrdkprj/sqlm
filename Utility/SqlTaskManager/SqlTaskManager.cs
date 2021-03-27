using MasudaManager.Utility.Preference;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace MasudaManager.Utility
{
    public class SqlTaskManager : ISqlTaskManager
    {
        IObserver _observer;
        IDbCommandBuilder _builder = new DbCommandBuilder();
        ISqlTask _currentTask = NullSqlTask.GetInstance();
        ISqlTaskFactory _factory;
        CancellationTokenSource _tokenSource = new CancellationTokenSource();
        SqlTaskRequest _request = new SqlTaskRequest();
        bool _detatching = false;

        public SqlTaskManager(IObserver observer, ISynchronizeInvoke invoker)
        {
            _observer = observer;

            _factory = new SqlTaskFactory(this, invoker);
        }

        public ISqlTaskFactory Factory { get { return _factory ?? NullSqlTaskFactory.Instance ; } }
        public Action<ISqlTask> TaskStartAction { get; set; }
        public Action<ISqlTask, TaskCompleteEventArgs> TaskCompleteAction { get; set; }
        public ISqlTask CurrentTask { get { return _currentTask; } }
        public SqlTaskOnErrorActionType DefaultErrorAction { get; set; }

        public async Task RunSqlTask(object guid, string sql, bool attachObserver)
        {
            await RunSqlTask(guid, PreapreDbCommandBuilder(sql), attachObserver);
        }

        IDbCommandBuilder PreapreDbCommandBuilder(string sql)
        {
            _builder.Clear();

            SqlType sqlType = SqlType.Empty;

            foreach (ParseContext context in SqlParser.Parse(sql))
            {
                sqlType = sqlType | context.SqlType;
                _builder.CreateCommand(context);
            }

            return _builder;
        }

        public async Task RunSqlTask(object guid, IDbCommandBuilder builder, bool attachObserver)
        {
            SqlType sqlType = SqlType.Empty;

            foreach (var command in builder)
            {
                sqlType = sqlType | command.SqlInfo.SqlType;
            }

            ISqlTask sqlTask = PrepareSqlTask(guid, sqlType, attachObserver);

            await Run(sqlTask, builder);
        }

        ISqlTask PrepareSqlTask(object guid, SqlType sqlType, bool attachObserver)
        {
            _request.RenewRequest(guid, sqlType, attachObserver);

            ISqlTask sqlTask = _factory.CreateSqlTask(_request);
            
            sqlTask.TaskStart -= SqlTask_TaskStart;
            sqlTask.TaskStart += SqlTask_TaskStart;
            sqlTask.TaskComplete -= SqlTask_TaskComplete;
            sqlTask.TaskComplete += SqlTask_TaskComplete;

            return sqlTask;
        }

        async Task Run(ISqlTask sqlTask, IDbCommandBuilder dbCommands)
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
        
        void SqlTask_TaskStart(object sender, EventArgs e)
        {
            if (this.TaskStartAction != null)
                this.TaskStartAction.Invoke((ISqlTask)sender);
        }

        void SqlTask_TaskComplete(object sender, TaskCompleteEventArgs e)
        {
            if (this.TaskCompleteAction != null)
                this.TaskCompleteAction.Invoke((ISqlTask)sender, e);
        }

        public void Release(IObserver observer)
        {
            SuspendObserve();

            this.TaskStartAction = null;
            this.TaskCompleteAction = null;

            foreach (var sqlTask in _factory.GetSqlTasks())
            {
                sqlTask.Detach(observer);
            }

            _factory.DisposeTask();

            Cancel();

            ResumeObserve();
        }

        void SuspendObserve()
        {
            _detatching = true;
        }

        void ResumeObserve()
        {
            _detatching = false;
        }

        public void FilterTaskResult(object guid, string keyString)
        {           
        }

        public void ClearFilter(object guid)
        {
        }

        public void Update(object sender)
        {
            if (_detatching)
                return;

            _observer.Update(sender);
        }

        public void Complete(object sender)
        {
            if (_detatching)
                return;

            _observer.Complete(sender);
        }

        public void Error(object sender, Exception e)
        {
            if (_detatching)
                return;

            if (!e.GetType().Equals(typeof(OperationCanceledException)))
                ActOnSqlTaskError();

            _observer.Error(sender, e);
        }

        void ActOnSqlTaskError()
        {
            switch (this.DefaultErrorAction)
            {
                case SqlTaskOnErrorActionType.None:
                    return;
                case SqlTaskOnErrorActionType.Cancel:
                    Cancel();
                    return;
                case SqlTaskOnErrorActionType.ComplyWithPreference:
                    if (!UserPreference.Setting.Sql.ContinueAfterError)
                        Cancel();
                    return;
            }
        }
    }
}
