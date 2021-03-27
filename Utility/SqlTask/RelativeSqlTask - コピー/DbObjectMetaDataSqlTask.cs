using MasudaManager.Enums;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Data;

namespace MasudaManager.Utility
{
    public abstract class DbObjectMetaDataSqlTask : QuerySqlTask, IFilterable
    {
        public override event EventHandler<TaskCompleteEventArgs> OnTaskComplete;

        public DbObjectMetaDataSqlTask(ISynchronizeInvoke invoker, ISqlService service) : base(invoker, service) { }

        #region Run
        protected override void InitializeSqlService()
        {
            _sqlService.Attach(this);
        }

        protected override Task ExecuteSqlService(IDbCommandBuilder dbCommands, CancellationToken token)
        {
            _bindingModel = new QueryResultBindingList(_invoker);

            return _sqlService.ExecuteSqlAsync(GetTypedDbCommand(dbCommands), token);
        }

        protected abstract IDbCommand GetTypedDbCommand(IDbCommandBuilder dbCommands);

        protected override void FinalizeSqlService()
        {
            if (NotifyOnTaskComplete)
                OnTaskComplete(this, new TaskCompleteEventArgs(this, false));

            _sqlService.Detach(this);
        }       
        #endregion

        #region OnSqlServiceUpdate
        protected override void OnSqlServiceUpdate()
        {
            _bindingModel.Add(_sqlService.CurrentSqlResult);
        }

        protected override void OnSqlServiceComplete()
        {
            _bindingModel.Save();
            _sqlService.Dispose();
            NotifyComplete();
        }

        protected override void OnSqlServiceError(Exception e)
        {
            Clear();
            _sqlService.Dispose();
            NotifyError(e);
        }
        #endregion

        #region Filter
        public abstract void Filter(string keystring);
        public abstract void ClearFilter();
        #endregion
    }
}
