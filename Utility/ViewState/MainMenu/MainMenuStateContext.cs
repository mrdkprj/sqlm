using MasudaManager.DataAccess;
using MasudaManager.Utility.Preference;
using MasudaManager.Utility.ViewState;
using MasudaManager.Views;
using System.Drawing;

namespace MasudaManager.Utility
{
    public class MainMenuStateContext
    {
        IViewState _currentState;
        IMainMenuView _view;
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();

        public MainMenuStateContext(IMainMenuView view)
        {
            _view = view;
            RestoreState();
        }

        public IViewState CurrentState { get { return _currentState; } }

        public void ChangeState()
        {
            if (_currentState == null)
            {
                if (_dataAccess.CurrentConnectionData.IsConnected)
                    _currentState = MainMenuLogOnState.Instance;
                else
                    _currentState = MainMenuLogOffState.Instance;
            }
            else
            {
                _currentState = _currentState.ChangeState();
            }

            ApplyState();
        }

        public void ChangeState(IViewState state)
        {
            _currentState = state;
            ApplyState();
        }

        public void ResetState()
        {
            _currentState = null;
            ChangeState();
        }
        
        public void RestoreState()
        {
            try
            {
                _dataAccess.Connect(UserPreference.State.LastDbConnection);
            }
            catch
            {
            }

            _view.CurrentWindowState = UserPreference.State.LastWindowState;
            _view.CurrentClientSize = UserPreference.State.LastClientSize;
        }

        public void SaveState()
        {
            if (_dataAccess.CurrentConnectionData.IsConnected)
                UserPreference.State.LastDbConnection = _dataAccess.CurrentConnectionData;
            else
                UserPreference.State.LastDbConnection = new ConnectionData();

            UserPreference.State.LastClientSize = _view.CurrentClientSize;
            UserPreference.State.LastWindowState = _view.CurrentWindowState;
            UserPreference.State.Save();
        }

        void ApplyState()
        {
            _view.ConnectButtonEnabled = _currentState.CanOpenConnection;
            _view.DisconnectButtonEnabled = _currentState.CanCloseConnection;
            _view.AddTabButtonEnabled = _currentState.CanExecuteSql;
            _view.ExecuteButtonEnabled = _currentState.CanExecuteSql;
            _view.CancelButtonEnabled = _currentState.CanCancelSql;
            _view.ExportButtonEnabled = _currentState.CanExecuteSql;
            _view.SearchButtonEnabled = _currentState.CanExecuteSql;
            _view.EditResultButtonEnabled = _currentState.CanChangeDbData;
            _view.PreferenceButtonEnabled = _currentState.CanExecuteSql;
            _view.ProgressBarVisible = _currentState.HasBusyProcess;

            if(_currentState.ViewText != null)
                _view.FormText = _currentState.ViewText;

            if (_currentState.StatusText != null)
            {
                _view.SetStatusLabelText(_currentState.StatusText);
                _view.SetStatusLabelColor(Color.SteelBlue);
            }
        }
    }
}
