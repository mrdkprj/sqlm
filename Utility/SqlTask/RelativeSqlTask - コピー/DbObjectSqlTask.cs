using MasudaManager.Enums;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MasudaManager.DataAccess;

namespace MasudaManager.Utility
{
    public class DbObjectSqlTask : SqlTask, IParentSqlTask
    {
        public override event EventHandler<TaskCompleteEventArgs> OnTaskComplete;

        List<SqlResult> _resultList = new List<SqlResult>();
        List<IChildSqlTask> _childSqlTaskList = new List<IChildSqlTask>();
        DbObjectPreparedDbCommandBuilder _typedSqlBuilder;
        DbObjectType _dbObjectType;
        readonly string _filterPropertyName = "Name";

        #region Property
        public List<IChildSqlTask> Children
        {
            get { return _childSqlTaskList; }
            set { _childSqlTaskList = value; }
        }
        public int CurrentIndex { get; set; }

        public object ChildPropertyType { get; set; }

        #endregion

        #region Constructor
        public DbObjectSqlTask(ISynchronizeInvoke invoker, ISqlService service) : base(invoker, service) { }
        public DbObjectSqlTask(ISynchronizeInvoke invoker, ISqlService service, DbObjectType dbObjectType)
            : base(invoker, service)
        {
            _invoker = invoker;
            _sqlService = service;
            _dbObjectType = dbObjectType;
            _bindingModel = new QueryResultBindingList(_invoker);
        }
        #endregion
     
        #region Call SQLService
        protected override void InitializeSqlService()
        {
            _sqlService.Attach(this);
        }

        protected override async Task ExecuteSqlService(IDbCommandBuilder dbCommands, CancellationToken token)
        {
            _typedSqlBuilder = new DbObjectPreparedDbCommandBuilder(_dbObjectType);
            _resultList = new List<SqlResult>();
            _bindingModel = new QueryResultBindingList(_invoker);

            await _sqlService.ExecuteSqlAsync(_typedSqlBuilder.CreateCommand(string.Empty).DbCommand, token);
        }

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
        #endregion

        #region フィルタリング
        public void Filter(string keystring)
        {
            _bindingModel.Filter(_filterPropertyName, keystring);
        }
        #endregion

        #region フィルタリング　クリア
        public void ClearFilter()
        {
        }
        #endregion

        #region Detach children
        public void ResetChildren()
        {
            foreach (ISqlTask sqlTask in _childSqlTaskList)
            {
                sqlTask.Clear();
            }

            _childSqlTaskList.Clear();           
        }
        #endregion

        #region Get children SqlStatement
        public IDbCommandBuilder GetSqlStatement()
        {
            if (_bindingModel.Count < CurrentIndex)
                CurrentIndex = _bindingModel.Count - 1;

            if (_bindingModel.Count > 0)
                _typedSqlBuilder.CreateCommand(GetDbObjectData(CurrentIndex));
            else
                _typedSqlBuilder.CreateCommand(string.Empty);

            return _typedSqlBuilder;
        }

        DbObjectData GetDbObjectData(int index)
        {
            return new DbObjectData(_bindingModel[index].ValueList[0], _bindingModel[index].ValueList[1]);
        }
        #endregion

        #region Children filter
        public void FilterChildren(string keyString)
        {
            foreach (var childTask in _childSqlTaskList)
            {
                childTask.Filter(keyString);
            }
        }

        public void ClearChildrenFilter()
        {
            foreach (var childTask in _childSqlTaskList)
            {
                childTask.ClearFilter();
            }
        }
        #endregion
    }
}
