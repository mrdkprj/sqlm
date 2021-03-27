using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using MasudaManager.Enums;

namespace MasudaManager.Utility
{
    public class TableColumnSqlTask : SqlTask, IRelativeSqlTask, IChildSqlTask
    {
                List<SqlResult> _resultList = new List<SqlResult>();
        readonly string _filterPropertyName = "Name";
        SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        public override event EventHandler<TaskCompleteEventArgs> OnTaskComplete;

        public TableColumnSqlTask(ISynchronizeInvoke invoker, ISqlService service) : base(invoker, service) { }

        public IRelativeSqlTask Child { get; set; }

        public string SetFilterPropertyname { get; set; }

        public void Filter(string keystring)
        {
            if (!String.IsNullOrEmpty(this.SetFilterPropertyname))
            {
                _bindingModel.Filter(this.SetFilterPropertyname, keystring);
            }
        }

        public void ClearFilter()
        {
        }

        protected override void InitializeSqlService()
        {
        }

        protected override async Task ExecuteSqlService(IDbCommandBuilder dbCommands, CancellationToken token)
        {
            _resultList = new List<SqlResult>();
            _bindingModel = new QueryResultBindingList(_invoker);

            //DbCommandSet command = dbCommands.Consume();
            //if(command != null)
            await _semaphore.WaitAsync().ConfigureAwait(false);
            try
            {
                _sqlService.Attach(this);
                await _sqlService.ExecuteSqlAsync(dbCommands.Consume().DbCommand, token);
            }
            finally
            {
                if (NotifyOnTaskComplete)
                    OnTaskComplete(this, new TaskCompleteEventArgs(this, false));

                _sqlService.Detach(this); 
                _semaphore.Release();
            }
        }

        protected override void FinalizeSqlService()
        {
        }

        protected override void OnSqlServiceUpdate()
        {
            _resultList.Add(_sqlService.CurrentSqlResult);
        }

        protected override void OnSqlServiceComplete()
        {
            _bindingModel.Clear();
            _bindingModel.AddRange(_resultList);
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

        public IParentSqlTask ParentSqlTask
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
    //public class TableColumnSqlTask : DbObjectMetaDataSqlTask, IChildSqlTask
    //{
    //    public TableColumnSqlTask(ISynchronizeInvoke invoker, ISqlService service) : base(invoker, service) { }
    //    readonly string _filterPropertyName = "Name";

    //    #region Property
    //    public IParentSqlTask ParentSqlTask { get; set; }
    //    #endregion

    //    protected override IDbCommand GetTypedDbCommand(IDbCommandBuilder dbCommands)
    //    {
    //        DbObjectPreparedDbCommandBuilder builder = dbCommands as DbObjectPreparedDbCommandBuilder;
    //        if (builder != null)
    //            return builder.GetTypedDbCommand(this);
    //        else
    //            throw new NullReferenceException("Failed to get DbObjectMetaData DbCommand");
    //    }

    //    public override void Filter(string keystring)
    //    {
    //        _bindingModel.Filter(_filterPropertyName, keystring);
    //    }

    //    public override void ClearFilter()
    //    {
    //    }
    //}
}
