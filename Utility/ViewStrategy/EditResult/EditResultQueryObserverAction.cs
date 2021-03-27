using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasudaManager.Utility.Preference;
using MasudaManager.Views;
using WinFormsMvp;

namespace MasudaManager.Utility
{
    class EditResultQueryObserverAction : IObserverActionStrategy<ISqlTask>
    {
        IEditResultView _view;
        bool _callbackRequired = false;

        static EditResultQueryObserverAction _instance = new EditResultQueryObserverAction();
        static EditResultQueryObserverAction() { }
        private EditResultQueryObserverAction() { }
        public static EditResultQueryObserverAction Instance
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
            _callbackRequired = true;
        }

        public void ActOnError(ISqlTask context)
        {
            if (!context.ExcutedSqlInfo.SqlException.GetType().Equals(typeof(OperationCanceledException)))
                _view.ShowMessage(context.ExcutedSqlInfo.SqlException.Message);

            _view.CloseAwaitDialog();
            _view.Model.OnQueryCancelled.Invoke();

            _callbackRequired = false;
        }
    }
}
