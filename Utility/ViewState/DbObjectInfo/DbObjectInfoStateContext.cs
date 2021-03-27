using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasudaManager.Views;
using MasudaManager.Utility.Preference;
using MasudaManager.DataAccess;
using MasudaManager.Utility.ViewState;

namespace MasudaManager.Utility
{
    public class DbObjectInfoStateContext
    {
        IViewState _currentState = null;
        IDbObjectInfoView _view;
        IDbObjectStrategy _strategy;
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();

        public DbObjectInfoStateContext(IDbObjectInfoView view)
        {
            _view = view;
        }

        public void ChangeState()
        {
            if (_currentState == null)
            {
                if (_dataAccess.CurrentConnectionData.IsConnected)
                    _currentState = DbObjectInfoLogOnState.Instance;
                else
                    _currentState = DbObjectInfoLogOffState.Instance;
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
        }

        public void ChangeState(IDbObjectStrategy strategy)
        {
            _strategy = strategy;
            if (_currentState != null)
                ApplyState();
        }

        void ApplyState()
        {
            _view.RefreshButtonEnabled = _currentState.CanExecuteSql;

            if (_currentState.CanExecuteSql)
                ChangeContextMenuItemState();
            else
                DisableContextMenu();
        }

        void DisableContextMenu()
        {
            _view.DisableContextMenu();
        }

        void ChangeContextMenuItemState()
        {
            _view.EnableContextMenu();

            if (_strategy.SupportDisplayData)
                _view.DisplayDataEnabled = _currentState.CanExecuteSql;
            else
                _view.DisplayDataEnabled = false;

            if (_strategy.SupportCreateSql)
                _view.CreateSqlEnabled = _currentState.CanExecuteSql;
            else
                _view.CreateSqlEnabled = false;

            if (_strategy.SupportEditData)
                _view.EditResultEnabled = _currentState.CanChangeDbData;
            else
                _view.EditResultEnabled = false;

            if (_strategy.SupportExport)
                _view.ExportEnabled = _currentState.CanExecuteSql;
            else
                _view.ExportEnabled = false;

            if (_strategy.SupportImport)
                _view.ImportEnabled = _currentState.CanChangeDbData;
            else
                _view.ImportEnabled = false;
        }
    }
}
