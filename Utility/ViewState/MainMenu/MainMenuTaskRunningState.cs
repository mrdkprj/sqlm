using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility.ViewState
{
    public class MainMenuTaskRunningState : IViewState
    {
        readonly string _statusLableText = "Executing...";

        static MainMenuTaskRunningState _instance = new MainMenuTaskRunningState();
        static MainMenuTaskRunningState() { }
        private MainMenuTaskRunningState() { }
        public static MainMenuTaskRunningState Instance
        {
            get { return _instance; }
        }

        public bool CanOpenConnection { get { return false; } }
        public bool CanCloseConnection { get { return false; } }
        public bool CanExecuteSql { get { return false; } }
        public bool CanCancelSql { get { return true; } }
        public bool CanChangeDbData { get { return false; } }
        public bool HasBusyProcess { get { return true ; } }
        public string ViewText { get { return null; } }
        public string StatusText { get { return _statusLableText; } }

        public IViewState ChangeState()
        {
            return MainMenuTaskCompleteState.Instance;
        }
    }
}
