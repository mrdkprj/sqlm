using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility.ViewState
{
    public class SqlTabTaskRunningState : IViewState
    {
        static SqlTabTaskRunningState _instance = new SqlTabTaskRunningState();
        static SqlTabTaskRunningState() { }
        private SqlTabTaskRunningState() { }
        public static SqlTabTaskRunningState Instance
        {
            get { return _instance; }
        }

        public bool CanOpenConnection { get { return false; } }
        public bool CanCloseConnection { get { return false; } }
        public bool CanExecuteSql { get { return false; } }
        public bool CanCancelSql { get { return true; } }
        public bool CanChangeDbData { get { return false; } }
        public bool HasBusyProcess { get { return true; } }
        public string ViewText { get { return null; } }
        public string StatusText { get { return null; } }

        public IViewState ChangeState()
        {
            return SqlTabLogOnState.Instance;
        }
    }
}
