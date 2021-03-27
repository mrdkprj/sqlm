using System.Collections.Generic;

namespace MasudaManager.Utility
{
    public interface IParentSqlTask : IFilterable
    {
        int CurrentIndex { get; set; }

        object ChildPropertyType { get; set; }

        List<IChildSqlTask> Children { get; set; }

        void ResetChildren();

        IDbCommandBuilder GetSqlStatement();

        void FilterChildren(string keyString);

        void ClearChildrenFilter();
    }
}
