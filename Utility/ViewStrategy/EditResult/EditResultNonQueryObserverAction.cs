using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasudaManager.Views;
using WinFormsMvp;

namespace MasudaManager.Utility
{
    class EditResultNonQueryObserverAction : IObserverActionStrategy<ISqlTask>
    {
        IEditResultView _view;
        bool _callbackRequired = false;

        static EditResultNonQueryObserverAction _instance = new EditResultNonQueryObserverAction();
        static EditResultNonQueryObserverAction() { }
        private EditResultNonQueryObserverAction() { }
        public static EditResultNonQueryObserverAction Instance
        {
            get { return _instance; }
        }

        public bool CallBackRequired { get { return _callbackRequired; } }

        public IView View
        {
            get { return _view; }
            set
            {
                if (value is IEditResultView)
                    _view = value as IEditResultView;
                else
                    _view = null;
            }
        }

        public void ActOnUpdate(ISqlTask context)
        {
            _callbackRequired = false;
        }

        public void ActOnComplete(ISqlTask context)
        {
            if (_view.Model.ApplyingCells.Count > 0)
                _view.Model.ApplyingCells.Dequeue();

            _callbackRequired = false;
        }

        public void ActOnError(ISqlTask context)
        {
            context.Rollback();

            _view.ShowMessage(context.ExcutedSqlInfo.SqlException.Message);
            _view.Model.HasApplyError = true;
            _view.FocusOnCell(_view.Model.ApplyingCells.Peek());

            _view.ApplyButtonEnabled = true;
            _view.CancelButtonEnabled = false;
            _view.InsertButtonEnabled = true;
            _view.DeleteButtonEnabled = true;
            _view.BulkInsertButtonEnabled = true;
            _view.UndoButtonEnabled = true;
            _view.RedoButtonEnabled = true;
            _view.MainGridViewEnabled = true;
            _view.ContextMenuEnabled = true;

            _callbackRequired = false;
        }

    }
}
