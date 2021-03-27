using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasudaManager.DataAccess;

namespace MasudaManager.Utility.ViewState
{
    public class MainMenuTaskCompleteState : IViewState
    {
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();

        static MainMenuTaskCompleteState _instance = new MainMenuTaskCompleteState();
        static MainMenuTaskCompleteState() { }
        private MainMenuTaskCompleteState() { }
        public static MainMenuTaskCompleteState Instance
        {
            get { return _instance; }
        }

        public bool CanOpenConnection { get { return true; } }
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
                return MainMenuTaskRunningState.Instance;
                
            return MainMenuLogOffState.Instance;
        }
    }
}
