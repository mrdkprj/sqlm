using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    public interface IViewState
    {
        bool CanOpenConnection { get; }
        bool CanCloseConnection { get; }
        bool CanExecuteSql { get; }
        bool CanCancelSql { get; }
        bool CanChangeDbData { get; }
        bool HasBusyProcess { get; }

        string ViewText { get; }
        string StatusText { get; }

        IViewState ChangeState();
    }
}
