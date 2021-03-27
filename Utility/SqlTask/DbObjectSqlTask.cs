using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    public class DbObjectSqlTask : SqlTask, IDbObjectSqlTask
    {
        List<SqlResult> _resultList = new List<SqlResult>();
        IDbObjectStrategy _strategy = null;

        public DbObjectSqlTask(ISynchronizeInvoke invoker, ISqlService service) : base(invoker, service) { }

        public IDbObjectStrategy Strategy
        {
            get { return _strategy; }
            set { _strategy = value; }
        }

        public void Filter(string keystring)
        {
            if (_strategy.SupportFilter)
                this.BindingList.Filter(_strategy.FilterableColumnIndex, keystring);
        }

        public void ClearFilter()
        {
        }

        protected override void InitializeSqlService()
        {
            OnTaskStart(this, EventArgs.Empty);
            this.Service.Attach(this);
        }

        protected override async Task ExecuteSqlService(IDbCommandBuilder dbCommands, CancellationToken token)
        {
            _resultList = new List<SqlResult>();
            this.BindingList = new QueryResultBindingList(this.Invoker);

            await this.Service.ExecuteSqlAsync(dbCommands.Consume().DbCommand, token);
        }

        protected override void FinalizeSqlService()
        {
            OnTaskComplete(this, new TaskCompleteEventArgs(SqlTaskResultType.DataFetched, this.Status));

            this.Service.Detach(this); 
        }

        protected override void OnSqlServiceUpdate()
        {
            _resultList.Add(this.Service.SqlResult);
        }

        protected override void OnSqlServiceComplete()
        {
            this.BindingList.Clear();
            this.BindingList.AddSourceRange(SqlResultFormatter.Convert(_strategy.PropertyType, _resultList));
            this.BindingList.Save();

            this.Service.Dispose();

            NotifyComplete();
        }

        protected override void OnSqlServiceError(Exception e)
        {
            Clear();
            this.Service.Dispose();
            NotifyError(e);
        }
    }
}
