using System;

namespace MasudaManager.Utility
{
    public interface IDbObjectSqlTask : ISqlTask
    {
        IDbObjectStrategy Strategy { get; set; }
        void Filter(string keystring);
        void ClearFilter();
    }
}
