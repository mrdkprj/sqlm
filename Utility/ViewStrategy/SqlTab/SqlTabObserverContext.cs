using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasudaManager.Views;
using WinFormsMvp;

namespace MasudaManager.Utility
{
    class SqlTabObserverContext : IObserverContext<ISqlTask>
    {
        ISqlTabView _view;
        ISqlTask _sqlTask;
        IObserverActionStrategy<ISqlTask> _strategy;
        List<Action<ISqlTask>> _callbackMethods = new List<Action<ISqlTask>>();

        public IView View
        {
            get { return _view; }
            set
            {
                if (value is ISqlTabView)
                    _view = value as ISqlTabView;
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
                    _strategy = SqlTabQueryObserverAction.Instance;
                    break;
                case SqlType.DDL:
                case SqlType.DML:
                case SqlType.TCL:
                    _strategy = SqlTabNonQueryObserverAction.Instance;
                    break;
                case SqlType.Invalid:
                    _strategy = SqlTabInvalidSqlObserverAction.Instance;
                    break;
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
