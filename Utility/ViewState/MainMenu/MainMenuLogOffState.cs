using MasudaManager.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility.ViewState
{
    public class MainMenuLogOffState : IViewState
    {
        readonly string _statusLableText = "Disconnected";
        readonly string _formCaptionText= "MSDMNG - DISCONNECTED";
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();

        static MainMenuLogOffState _instance = new MainMenuLogOffState();
        static MainMenuLogOffState() { }
        private MainMenuLogOffState() { }
        public static MainMenuLogOffState Instance
        {
            get { return _instance; }
        }

        public bool CanOpenConnection { get { return true; } }
        public bool CanCloseConnection { get { return false; } }
        public bool CanExecuteSql { get { return false; } }
        public bool CanCancelSql { get { return false; } }
        public bool CanChangeDbData { get { return false; } }
        public bool HasBusyProcess { get { return false; } }
        public string ViewText { get { return _formCaptionText; } }
        public string StatusText { get { return _statusLableText; } }

        public IViewState ChangeState()
        {
            if (_dataAccess.CurrentConnectionData.IsConnected)
                return MainMenuLogOnState.Instance;

            return this;
        }
    }
}
