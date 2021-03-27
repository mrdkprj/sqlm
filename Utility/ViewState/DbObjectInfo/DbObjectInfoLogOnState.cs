using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasudaManager.DataAccess;

namespace MasudaManager.Utility.ViewState
{
    public class DbObjectInfoLogOnState : IViewState
    {
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();

        static DbObjectInfoLogOnState _instance = new DbObjectInfoLogOnState();
        static DbObjectInfoLogOnState() { }
        private DbObjectInfoLogOnState() { }
        public static DbObjectInfoLogOnState Instance
        {
            get { return _instance; }
        }

        public bool CanOpenConnection { get { return false; } }
        public bool CanCloseConnection { get { return true; } }
        public bool CanExecuteSql { get { return true; } }
        public bool CanCancelSql { get { return false; } }
        public bool CanChangeDbData { get { return true; } }
        public bool HasBusyProcess { get { return false; } }
        public string ViewText { get { return null; } }
        public string StatusText { get { return null; } }

        public IViewState ChangeState()
        {
            if (_dataAccess.CurrentConnectionData.IsConnected)
                return DbObjectInfoTaskRunningState.Instance;

            return DbObjectInfoLogOffState.Instance;
        }
    }
}
