using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace MasudaManager.Utility
{
    public class NonQuerySqlTask : SqlTask
    {
        SqlTaskResultType _resultType = SqlTaskResultType.DataAltered;
        
        public NonQuerySqlTask(ISynchronizeInvoke invoker, ISqlService service) : base(invoker, service) { }
        
        protected override void InitializeSqlService()
        {
            OnTaskStart(this, EventArgs.Empty);
            this.Service.Attach(this);
        }

        protected override async Task ExecuteSqlService(IDbCommandBuilder builder, CancellationToken token)
        {
            foreach (var commandSet in builder)
            {
                if (token.IsCancellationRequested)
                    break;

                PrepareForExecute(commandSet);

                await this.Service.ExecuteSqlAsync(commandSet.DbCommand, token);
            }
        }

        void PrepareForExecute(DbCommandSet commandSet)
        {
            this.BindingList = new QueryResultBindingList(this.Invoker);
            this.SqlBaseInfo = commandSet.SqlInfo;

            if (this.SqlBaseInfo.SqlType == SqlType.DDL)
                _resultType = SqlTaskResultType.DefinitionAltered;
        }

        protected override void FinalizeSqlService()
        {
            OnTaskComplete(this, new TaskCompleteEventArgs(_resultType, this.Status));

            this.Service.Detach(this);
        }

        protected override void OnSqlServiceUpdate()
        {
            this.BindingList.AddSource(this.Service.SqlResult);

            this.SqlBaseInfo.HasRows = (this.BindingList.Count > 0);
            this.SqlBaseInfo.HasError = false;
            this.SqlBaseInfo.RecordCount = this.Service.SqlResult.AffectedRowCount;

            Notify();
        }

        protected override void OnSqlServiceComplete()
        {
            this.SqlBaseInfo.HasRows = (this.BindingList.Count > 0);
            this.SqlBaseInfo.HasError = false;
            this.SqlBaseInfo.RecordCount = this.Service.SqlResult.AffectedRowCount;

            this.SqlBaseInfo.SqlMessage = this.MessageProvider.GetMessage(this.SqlBaseInfo);

            this.Service.Dispose();

            NotifyComplete();
        }

        protected override void OnSqlServiceError(Exception e)
        {
            this.SqlBaseInfo.HasError = true;
            this.SqlBaseInfo.SqlMessage = e.Message;
            this.SqlBaseInfo.SqlException = e;

            this.Service.Dispose();

            NotifyError(e);
        }
    }
}
