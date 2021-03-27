using System;
using System.ComponentModel;
using System.Data;

namespace MasudaManager.Utility
{
    public class GeneralPropertySqlTask : DbObjectMetaDataSqlTask, IChildSqlTask
    {
        public GeneralPropertySqlTask(ISynchronizeInvoke invoker, ISqlService service) : base(invoker, service) { }

        #region Property
        public IParentSqlTask ParentSqlTask { get; set; }
        #endregion
        
        protected override IDbCommand GetTypedDbCommand(IDbCommandBuilder dbCommands)
        {
            DbObjectPreparedDbCommandBuilder builder = dbCommands as DbObjectPreparedDbCommandBuilder;
            if (builder != null)
                return builder.GetTypedDbCommand(this);
            else
                throw new NullReferenceException("Failed to get DbObjectMetaData DbCommand");
        }

        public override void Filter(string keystring)
        {
        }

        public override void ClearFilter()
        {
        }
    }
}
