using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasudaManager.Views;
using WinFormsMvp;

namespace MasudaManager.Utility
{
    class EditResultObserverContext : IObserverContext<ISqlTask>
    {
        IEditResultView _view;
        ISqlTask _sqlTask;
        IObserverActionStrategy<ISqlTask> _strategy;
        List<Action<ISqlTask>> _callbackMethods = new List<Action<ISqlTask>>();

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

        public void RegisterCallBack(Action<ISqlTask> callbackMethod)
        {
            _callbackMethods.Add(callbackMethod);
        }

        public void ChangeStrategy(ISqlTask sqlTask)
        {
            _sqlTask = sqlTask;

            switch (_sqlTask.ExcutedSqlInfo.SqlType)
            {
                case SqlType.Query:
                    _strategy = EditResultQueryObserverAction.Instance;
                    break;
                case SqlType.DML:
                    _strategy = EditResultNonQueryObserverAction.Instance;
                    break;
                default:
                    return;
            }

            _strategy.View = _view;
        }

        public void Act(ObserverActionType actionType)
        {
            switch (actionType)
            {
                case ObserverActionType.OnUpdate:
                    ActOnUpdate();
                    break;
                case ObserverActionType.OnComplete:
                    ActOnComplete();
                    break;
                case ObserverActionType.OnError:
                    ActOnError();
                    break;
            }
        }

        public void ActOnUpdate()
        {
            _strategy.ActOnUpdate(_sqlTask);
            DoCallBack();
        }

        public void ActOnComplete()
        {
            _strategy.ActOnComplete(_sqlTask);
            DoCallBack();
        }

        public void ActOnError()
        {
            _strategy.ActOnError(_sqlTask);
            DoCallBack();
        }

        void DoCallBack()
        {
            if (_strategy.CallBackRequired && _callbackMethods.Count > 0)
                _callbackMethods.ForEach(s => s.Invoke(_sqlTask));
        }
    }
}
