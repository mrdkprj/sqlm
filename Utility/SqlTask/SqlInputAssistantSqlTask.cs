using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    public class SqlInputAssistantSqlTask : SqlTask
    {
        readonly string _filterPropertyName = "Name";

        string _filterString = null;
        List<SqlResult> _resultSetList = new List<SqlResult>();

        public SqlInputAssistantSqlTask(ISynchronizeInvoke invoker, ISqlService service) : base(invoker, service) { }

        protected override void InitializeSqlService()
        {
            this.Service.NotifyOnUpdate = true;
            this.Service.Attach(this);
        }

        protected override async Task ExecuteSqlService(IDbCommandBuilder dbCommands, CancellationToken token)
        {
            _resultSetList = new List<SqlResult>();
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
            _resultSetList.Add(this.Service.SqlResult);
        }

        protected override void OnSqlServiceComplete()
        {
            this.BindingList.Clear();

            if (this.Service is QuerySqlService)
                this.BindingList.AddSourceRange(_resultSetList);
            else
                this.BindingList.AddSourceRange(SqlInputAssistantDataFormatter.Instance.Format(_resultSetList));

            this.BindingList.Save();

            this.Service.Dispose();

            NotifyComplete();
        }

        protected override void OnSqlServiceError(Exception e)
        {
            this.BindingList.Clear();
            this.Service.Dispose();
            NotifyError(e);
        }

        public void Filter(string keystring)
        {
            _filterString += keystring;

            this.BindingList.Filter(_filterPropertyName, _filterString);
        }

        public void ClearFilter()
        {
            if (this.BindingList.CanRestore)
            {
                this.BindingList.Restore();
                _filterString = string.Empty;
            }
            else
            {
                this.BindingList.Clear();
            }
        }
    }
}
