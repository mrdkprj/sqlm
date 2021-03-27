using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasudaManager.DataAccess;

namespace MasudaManager.Utility.ViewState
{
    public class MainMenuLogOnState : IViewState
    {
        readonly string _statusLableText = string.Empty;
        readonly string _formCaptionFormat = "MSDMNG - {0}/{1}";
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();
        
        static MainMenuLogOnState _instance = new MainMenuLogOnState();
        static MainMenuLogOnState() { }
        private MainMenuLogOnState() { }
        public static MainMenuLogOnState Instance
        {
            get { return _instance; }
        }

        public bool CanOpenConnection { get { return true; } }
        public bool CanCloseConnection { get { return true; } }
        public bool CanExecuteSql { get { return true; } }
        public bool CanCancelSql { get { return false; } }
        public bool CanChangeDbData { get { return true; } }
        public bool HasBusyProcess { get { return false; } }
        public string ViewText { get { return GetFormCaption(); } }
        public string StatusText { get { return _statusLableText; } }

        public IViewState ChangeState()
        {
            if (_dataAccess.CurrentConnectionData.IsConnected)
                return MainMenuTaskRunningState.Instance;

            return MainMenuLogOffState.Instance;
        }

        string GetFormCaption()
        {
            return String.Format(_formCaptionFormat, _dataAccess.CurrentConnectionData.UserId, _dataAccess.CurrentConnectionData.DataSource);
        }

    }
}
