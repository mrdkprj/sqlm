using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasudaManager.Views;
using WinFormsMvp;

namespace MasudaManager.Utility
{
    class SqlTabInvalidSqlObserverAction : IObserverActionStrategy<ISqlTask>
    {
        ISqlTabView _view;
        bool _callbackRequired = false;

        static SqlTabInvalidSqlObserverAction _instance = new SqlTabInvalidSqlObserverAction();
        static SqlTabInvalidSqlObserverAction() { }
        private SqlTabInvalidSqlObserverAction() { }
        public static SqlTabInvalidSqlObserverAction Instance
        {
            get { return _instance; }
        }

        public bool CallBackRequired { get { return _callbackRequired; } }

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

        public void ActOnUpdate(ISqlTask context)
        {
            _callbackRequired = false;
        }

        public void ActOnComplete(ISqlTask context)
        {
            _callbackRequired = false;
        }

        public void ActOnError(ISqlTask context)
        {
            WriteLogView(context.Guid, context.ExcutedSqlInfo.InputSql, false, false);
            WriteLogView(context.Guid, context.ExcutedSqlInfo.SqlMessage, false, true);
            _view.BringLogViewFront(context.Guid);
            ParseErrorException parseError = context.ExcutedSqlInfo.SqlException.InnerException as ParseErrorException;
            if (parseError != null)
                _view.SetInputViewSelection(context.Guid, parseError.Position);
            _view.SetInputViewFocus(context.Guid);
            _callbackRequired = true;
        }

        void WriteLogView(object guid, string message, bool writeDate, bool writeLine)
        {
            if (writeDate)
                _view.SetLogViewText(guid, message, DateTime.Now, writeLine);
            else
                _view.SetLogViewText(guid, message, null, writeLine);
        }
    }
}
