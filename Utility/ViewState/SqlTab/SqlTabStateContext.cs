using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasudaManager.Views;
using MasudaManager.Utility.Preference;
using MasudaManager.DataAccess;
using MasudaManager.Utility.ViewState;
using System.Drawing;

namespace MasudaManager.Utility
{
    public class SqlTabStateContext
    {
        IViewState _currentState;
        ISqlTabView _view;
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();

        public SqlTabStateContext(ISqlTabView view)
        {
            _view = view;
        }

        public IViewState CurrentState { get { return _currentState; } }

        public void ChangeState()
        {
            if (_currentState == null)
            {
                if (_dataAccess.CurrentConnectionData.IsConnected)
                    _currentState = SqlTabLogOnState.Instance;
                else
                    _currentState = SqlTabLogOffState.Instance;
            }
            else
            {
                _currentState = _currentState.ChangeState();
            }

            ApplyState();
        }

        public void ResetState()
        {
            _currentState = null;
            ChangeState();
        }

        public void ChangeState(IViewState state)
        {
            _currentState = state;
            ApplyState();
        }

        void ApplyState()
        {
            if (_currentState.HasBusyProcess)
                DisableContextMenu();
            else
                EnableContextMenu();
        }

        void DisableContextMenu()
        {
            _view.DisableResultViewContextMenu();
        }

        void EnableContextMenu()
        {
            _view.EnableResultViewContextMenu();

            if (_currentState.CanChangeDbData)
                _view.EnableEditResult();
            else
                _view.DisableEditResult();
        }
    }
}
