using MasudaManager.Enums;
using System;
using System.ComponentModel;
using System.Data;

namespace MasudaManager.Utility
{
    public class TableIndexSqlTask : DbObjectMetaDataSqlTask, IChildSqlTask
    {
        public TableIndexSqlTask(ISynchronizeInvoke invoker, ISqlService service) : base(invoker, service) { }

        #region Property
        public IParentSqlTask ParentSqlTask { get; set; }
        #endregion
        
        protected override IDbCommand GetTypedDbCommand(IDbCommandBuilder dbCommands)
        {
            //DbObjectPreparedDbCommandBuilder builder = dbCommands as DbObjectPreparedDbCommandBuilder;
            //if (builder != null)
            //    return builder.GetTypedDbCommand(this);
            //else
            //    throw new NullReferenceException("Failed to get DbObjectMetaData DbCommand");
            return dbCommands.Consume().DbCommand;

        }

        //protected override void CreateModel()
        //{
        //    string name = string.Empty;

        //    for (int i = 0; i < _resultSetList.Count; i++)
        //    {
        //        if (name == _resultSetList[i].RowValues[(int)IndexDataName.IndexName])
        //        {
        //            _resultSetList[i].RowValues[(int)IndexDataName.IndexName] = string.Empty;
        //        }
        //        else
        //        {
        //            name = _resultSetList[i].RowValues[(int)IndexDataName.IndexName];
        //        }

        //        _bindingModel.Add(new TableIndexData(
        //                           _resultSetList[i].RowValues[(int)IndexDataName.IndexName],
        //                           _resultSetList[i].RowValues[(int)IndexDataName.IndexNo],
        //                           _resultSetList[i].RowValues[(int)IndexDataName.ColumnName]
        //                           ));
        //    }
        //}

        public override void Filter(string keystring)
        {
        }

        public override void ClearFilter()
        {
        }
    }
}
